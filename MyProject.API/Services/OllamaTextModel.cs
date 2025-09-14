using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MyProject.Domain.Interfaces;

namespace MyProject.API.Services
{
    public class OllamaTextModel : ITextModel
    {
        private readonly HttpClient _httpClient;

        public OllamaTextModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("Ollama");
        }

        public async Task<string> GenerateAsync(string prompt)
        {
            var requestBody = new
            {
                model = "phi3",
                stream = true,
                prompt = $@"
You are an AI calendar assistant.
Respond ONLY with a single valid JSON object.
Do not include explanations or extra text.

Format (strict JSON):
{{
  ""title"": string,
  ""start"": ""YYYY-MM-DDTHH:MM:SS"",
  ""end"": ""YYYY-MM-DDTHH:MM:SS"",
  ""duration_minutes"": number,
  ""attendees"": [ string ],
  ""location"": string
}}

Rules:
- Title should describe the meeting (e.g., ""Meeting with Sarah"").
- Extract attendees from text (e.g., ""Sarah and John"" → [""Sarah"", ""John""]).
- If no attendees, use [].
- If duration is mentioned (""30 minutes"", ""2 hours""), calculate accordingly.
- If ""till X"" is mentioned, use that as end time.
- If no duration/end is given, default to 1 hour.
- If no location is given, set to """".
- Always use today's date if none specified.
- Use 24-hour format with seconds.
- Output ONLY JSON, nothing else.

Now convert this request:
""{prompt}""
"
            };

            var json = JsonSerializer.Serialize(requestBody);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/generate")
            {
                Content = httpContent
            };

            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();

            await using var stream = await response.Content.ReadAsStreamAsync();
            using var reader = new StreamReader(stream);

            var sb = new StringBuilder();
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (string.IsNullOrWhiteSpace(line)) continue;

                try
                {
                    using var doc = JsonDocument.Parse(line);
                    if (doc.RootElement.TryGetProperty("response", out var respProp))
                    {
                        sb.Append(respProp.GetString());
                    }
                }
                catch
                {
                    // Ignore malformed fragments
                }
            }

            var fullResponse = sb.ToString().Trim();
            Console.WriteLine("Ollama combined response: " + fullResponse);

            if (string.IsNullOrWhiteSpace(fullResponse))
            {
                return FallbackJson();
            }

            if (fullResponse.TrimStart().StartsWith("{"))
            {
                try
                {
                    using var doc = JsonDocument.Parse(fullResponse);
                    var root = doc.RootElement;

                    // Validate fields
                    string title = root.TryGetProperty("title", out var titleProp)
                        ? titleProp.GetString() ?? "Meeting"
                        : "Meeting";

                    string startStr = root.TryGetProperty("start", out var startProp)
                        ? startProp.GetString() ?? ""
                        : "";

                    string endStr = root.TryGetProperty("end", out var endProp)
                        ? endProp.GetString() ?? ""
                        : "";

                    int durationMinutes = root.TryGetProperty("duration_minutes", out var durProp) && durProp.TryGetInt32(out var durVal)
                        ? durVal
                        : 60;

                    var attendees = root.TryGetProperty("attendees", out var attProp) && attProp.ValueKind == JsonValueKind.Array
                        ? attProp.EnumerateArray().Select(a => a.GetString() ?? "").Where(a => !string.IsNullOrWhiteSpace(a)).ToArray()
                        : Array.Empty<string>();

                    string location = root.TryGetProperty("location", out var locProp)
                        ? locProp.GetString() ?? ""
                        : "";

                    DateTime start, end;

                    if (!DateTime.TryParse(startStr, out start))
                        start = DateTime.Today.AddHours(15); // default 3pm today

                    if (!DateTime.TryParse(endStr, out end))
                        end = start.AddMinutes(durationMinutes);

                    if (start.Date < DateTime.Today)
                        start = DateTime.Today.Add(start.TimeOfDay);

                    if (end <= start)
                        end = start.AddMinutes(durationMinutes);

                    return JsonSerializer.Serialize(new
                    {
                        title,
                        start = start.ToString("yyyy-MM-ddTHH:mm:ss"),
                        end = end.ToString("yyyy-MM-ddTHH:mm:ss"),
                        duration_minutes = (int)(end - start).TotalMinutes,
                        attendees,
                        location
                    });
                }
                catch
                {
                    return FallbackJson();
                }
            }

            return FallbackJson();
        }

        private string FallbackJson()
        {
            return JsonSerializer.Serialize(new
            {
                title = "Meeting",
                start = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"),
                end = DateTime.Now.AddHours(1).ToString("yyyy-MM-ddTHH:mm:ss"),
                duration_minutes = 60,
                attendees = Array.Empty<string>(),
                location = ""
            });
        }
    }
}

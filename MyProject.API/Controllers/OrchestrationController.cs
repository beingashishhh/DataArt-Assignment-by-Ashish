using Microsoft.AspNetCore.Mvc;
using MyProject.Domain.Interfaces;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyProject.API.Controllers
{
    [ApiController]
    [Route("api/orchestration")]   
    public class OrchestrationController : ControllerBase
    {
        private readonly ITextModel _textModel;
        private readonly HttpClient _mcpClient;

        public OrchestrationController(ITextModel textModel, IHttpClientFactory httpClientFactory)
        {
            _textModel = textModel;
            _mcpClient = httpClientFactory.CreateClient("MCP");
        }

        [HttpPost("process")]
        public async Task<IActionResult> Process([FromBody] PromptRequest request)
        {
            var structuredJson = await _textModel.GenerateAsync(request.Prompt);

            var content = new StringContent(structuredJson, Encoding.UTF8, "application/json");
            var response = await _mcpClient.PostAsync("/save_event", content);

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());

            var result = await response.Content.ReadAsStringAsync();
            return Ok(JsonDocument.Parse(result).RootElement);
        }
    }

    public class PromptRequest
    {
        public required string Prompt { get; set; }
    }
}

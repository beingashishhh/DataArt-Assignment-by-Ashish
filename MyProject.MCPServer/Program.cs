using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/save_event", async (HttpRequest req, HttpResponse res) =>
{
    using var doc = await JsonDocument.ParseAsync(req.Body);
    // Just echo back the parsed JSON with an added "saved": true
    var root = doc.RootElement;
    var outObj = new
    {
        saved = true,
        payload = root
    };
    res.ContentType = "application/json";
    await res.WriteAsync(JsonSerializer.Serialize(outObj));
});

app.MapPost("/update_event", async (HttpRequest req, HttpResponse res) =>
{
    using var doc = await JsonDocument.ParseAsync(req.Body);
    var root = doc.RootElement;
    var outObj = new { updated = true, payload = root };
    res.ContentType = "application/json";
    await res.WriteAsync(JsonSerializer.Serialize(outObj));
});

app.MapPost("/cancel_event", async (HttpRequest req, HttpResponse res) =>
{
    using var doc = await JsonDocument.ParseAsync(req.Body);
    var root = doc.RootElement;
    var outObj = new { canceled = true, payload = root };
    res.ContentType = "application/json";
    await res.WriteAsync(JsonSerializer.Serialize(outObj));
});

app.Run("http://localhost:5035");

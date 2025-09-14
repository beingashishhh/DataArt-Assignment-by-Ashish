using Microsoft.EntityFrameworkCore;
using MyProject.MCPServer.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=events.db"));

var app = builder.Build();

// SAVE
app.MapPost("/save_event", async (AppDbContext db, Event request) =>
{
    // Auto-calculate duration
    if (request.Start != default && request.End != default)
    {
        request.DurationMinutes = (int)(request.End - request.Start).TotalMinutes;
    }

    db.Events.Add(request);
    await db.SaveChangesAsync();

    return Results.Json(new
    {
        saved = true,
        payload = request
    });
});

// UPDATE
app.MapPost("/update_event", async (AppDbContext db, Event request) =>
{
    var existing = await db.Events.FindAsync(request.Id);
    if (existing is null)
        return Results.NotFound(new { updated = false, message = "Event not found" });

    existing.Title = request.Title;
    existing.Start = request.Start;
    existing.End = request.End;

    // Auto-calculate duration on update too
    if (existing.Start != default && existing.End != default)
    {
        existing.DurationMinutes = (int)(existing.End - existing.Start).TotalMinutes;
    }

    existing.Attendees = request.Attendees;
    existing.Location = request.Location;

    await db.SaveChangesAsync();

    return Results.Json(new
    {
        updated = true,
        payload = existing
    });
});

// CANCEL
app.MapPost("/cancel_event", async (AppDbContext db, Event request) =>
{
    var existing = await db.Events.FindAsync(request.Id);
    if (existing is null)
        return Results.NotFound(new { canceled = false, message = "Event not found" });

    db.Events.Remove(existing);
    await db.SaveChangesAsync();

    return Results.Json(new
    {
        canceled = true,
        payload = existing
    });
});

// GET
app.MapGet("/get_events", async (AppDbContext db) =>
{
    var events = await db.Events.ToListAsync();
    return Results.Json(events);
});

app.Run("http://localhost:5035");

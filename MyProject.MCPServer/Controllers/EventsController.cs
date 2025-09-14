using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.MCPServer.Data;

namespace MyProject.MCPServer.Controllers
{
    [ApiController]
    [Route("/")]
    public class EventsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public EventsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpPost("save_event")]
        public async Task<IActionResult> SaveEvent([FromBody] Event request)
        {
            _db.Events.Add(request);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                saved = true,
                payload = request
            });
        }

        [HttpGet("get_events")]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _db.Events.ToListAsync();
            return Ok(events);
        }
    }
}

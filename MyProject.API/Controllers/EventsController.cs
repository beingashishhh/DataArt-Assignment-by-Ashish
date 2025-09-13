using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Domain.Entities;
using MyProject.Domain.Interfaces;

namespace MyProject.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;

        public EventsController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var events = await _eventRepository.GetAllAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var evt = await _eventRepository.GetByIdAsync(id);
            if (evt == null) return NotFound();
            return Ok(evt);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Event evt)
        {
            await _eventRepository.AddAsync(evt);
            return CreatedAtAction(nameof(GetById), new { id = evt.Id }, evt);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Event evt)
        {
            if (id != evt.Id) return BadRequest();

            var existing = await _eventRepository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Name = evt.Name;
            existing.Description = evt.Description;

            var updated = await _eventRepository.UpdateAsync(existing);
            if (!updated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _eventRepository.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}

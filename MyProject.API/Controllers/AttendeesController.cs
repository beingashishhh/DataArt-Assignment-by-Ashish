using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Domain.Entities;
using MyProject.Domain.Interfaces;

namespace MyProject.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AttendeesController : ControllerBase
    {
        private readonly IAttendeeRepository _attendeeRepository;

        public AttendeesController(IAttendeeRepository attendeeRepository)
        {
            _attendeeRepository = attendeeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var attendees = await _attendeeRepository.GetAllAsync();
            return Ok(attendees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var attendee = await _attendeeRepository.GetByIdAsync(id);
            if (attendee == null) return NotFound();
            return Ok(attendee);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Attendee attendee)
        {
            await _attendeeRepository.AddAsync(attendee);
            return CreatedAtAction(nameof(GetById), new { id = attendee.Id }, attendee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Attendee attendee)
        {
            if (id != attendee.Id) return BadRequest();

            var existing = await _attendeeRepository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Name = attendee.Name;
            existing.Email = attendee.Email;

            var updated = await _attendeeRepository.UpdateAsync(existing);
            if (!updated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _attendeeRepository.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}

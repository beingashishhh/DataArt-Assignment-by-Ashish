using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Domain.Entities;
using MyProject.Domain.Interfaces;

namespace MyProject.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CalendarsController : ControllerBase
    {
        private readonly ICalendarRepository _calendarRepository;

        public CalendarsController(ICalendarRepository calendarRepository)
        {
            _calendarRepository = calendarRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var calendars = await _calendarRepository.GetAllAsync();
            return Ok(calendars);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var calendar = await _calendarRepository.GetByIdAsync(id);
            if (calendar == null) return NotFound();
            return Ok(calendar);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Calendar calendar) 
        {
            await _calendarRepository.AddAsync(calendar);
            return CreatedAtAction(nameof(GetById), new { id = calendar.Id }, calendar);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Calendar calendar) 
        {
            if (id != calendar.Id) return BadRequest();

            var existing = await _calendarRepository.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Name = calendar.Name;
            existing.Description = calendar.Description;

            var updated = await _calendarRepository.UpdateAsync(existing);
            if (!updated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _calendarRepository.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        } 
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyProject.Domain.Entities;
using MyProject.Domain.Interfaces;

namespace MyProject.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MeetingsController : ControllerBase
    {
        private readonly IMeetingRepository _meetingRepository;

        public MeetingsController(IMeetingRepository meetingRepository)
        {
            _meetingRepository = meetingRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var meetings = await _meetingRepository.GetAllAsync();
            return Ok(meetings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var meeting = await _meetingRepository.GetByIdAsync(id);
            if (meeting == null) return NotFound();
            return Ok(meeting);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Meeting meeting)
        {
            var created = await _meetingRepository.AddAsync(meeting);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Meeting meeting)
        {
            if (id != meeting.Id) return BadRequest();

            var updated = await _meetingRepository.UpdateAsync(meeting);
            if (!updated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _meetingRepository.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}

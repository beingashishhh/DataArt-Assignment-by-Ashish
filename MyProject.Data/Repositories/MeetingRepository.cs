using Microsoft.EntityFrameworkCore;
using MyProject.Data.Context;
using MyProject.Domain.Entities;
using MyProject.Domain.Interfaces;

namespace MyProject.Data.Repositories
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly AppDbContext _context;

        public MeetingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Meeting>> GetAllAsync()
        {
            return await _context.Meetings.ToListAsync();
        }

        public async Task<Meeting?> GetByIdAsync(int id)
        {
            return await _context.Meetings.FindAsync(id);
        }

        public async Task<Meeting> AddAsync(Meeting meeting)
        {
            _context.Meetings.Add(meeting);
            await _context.SaveChangesAsync();
            return meeting;
        }

        public async Task<bool> UpdateAsync(Meeting meeting)
        {
            if (!await _context.Meetings.AnyAsync(m => m.Id == meeting.Id))
                return false;

            _context.Meetings.Update(meeting);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var meeting = await _context.Meetings.FindAsync(id);
            if (meeting == null) return false;

            _context.Meetings.Remove(meeting);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

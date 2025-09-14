using Microsoft.EntityFrameworkCore;
using MyProject.Data.Context;
using MyProject.Domain.Entities;
using MyProject.Domain.Interfaces;

namespace MyProject.Data.Repositories
{
    public class AttendeeRepository : IAttendeeRepository
    {
        private readonly AppDbContext _context;

        public AttendeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Attendee>> GetAllAsync()
        {
            return await _context.Attendees.ToListAsync();
        }

        public async Task<Attendee?> GetByIdAsync(int id)
        {
            return await _context.Attendees.FindAsync(id);
        }

        public async Task AddAsync(Attendee attendee)
        {
            _context.Attendees.Add(attendee);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Attendee attendee)
        {
            var exists = await _context.Attendees.AnyAsync(a => a.Id == attendee.Id);
            if (!exists) return false;

            _context.Attendees.Update(attendee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var attendee = await _context.Attendees.FindAsync(id);
            if (attendee == null) return false;

            _context.Attendees.Remove(attendee);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

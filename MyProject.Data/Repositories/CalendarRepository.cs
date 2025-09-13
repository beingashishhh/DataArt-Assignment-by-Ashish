using Microsoft.EntityFrameworkCore;
using MyProject.Domain.Entities;
using MyProject.Domain.Interfaces;
using MyProject.Data.Context;

namespace MyProject.Data.Repositories
{
    public class CalendarRepository : ICalendarRepository
    {
        private readonly AppDbContext _context;

        public CalendarRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Calendar>> GetAllAsync()
        {
            return await _context.Calendars.ToListAsync();
        }

        public async Task<Calendar?> GetByIdAsync(int id)
        {
            return await _context.Calendars.FindAsync(id);
        }

        public async Task AddAsync(Calendar calendar)
        {
            _context.Calendars.Add(calendar);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Calendar calendar)
        {
            var exists = await _context.Calendars.AnyAsync(c => c.Id == calendar.Id);
            if (!exists) return false;

            _context.Calendars.Update(calendar);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var calendar = await _context.Calendars.FindAsync(id);
            if (calendar == null) return false;

            _context.Calendars.Remove(calendar);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

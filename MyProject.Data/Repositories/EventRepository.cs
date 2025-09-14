using Microsoft.EntityFrameworkCore;
using MyProject.Data.Context;
using MyProject.Domain.Entities;
using MyProject.Domain.Interfaces;

namespace MyProject.Data.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task AddAsync(Event evt)
        {
            _context.Events.Add(evt);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Event evt)
        {
            var exists = await _context.Events.AnyAsync(e => e.Id == evt.Id);
            if (!exists) return false;

            _context.Events.Update(evt);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var evt = await _context.Events.FindAsync(id);
            if (evt == null) return false;

            _context.Events.Remove(evt);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

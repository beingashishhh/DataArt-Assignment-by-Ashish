using MyProject.Domain.Entities;

namespace MyProject.Domain.Interfaces
{
    public interface IAttendeeRepository
    {
        Task<IEnumerable<Attendee>> GetAllAsync();
        Task<Attendee?> GetByIdAsync(int id);
        Task AddAsync(Attendee attendee);
        Task<bool> UpdateAsync(Attendee attendee);
        Task<bool> DeleteAsync(int id);
    }
}

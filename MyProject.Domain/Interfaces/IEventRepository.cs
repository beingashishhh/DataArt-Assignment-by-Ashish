using MyProject.Domain.Entities;

namespace MyProject.Domain.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllAsync();
        Task<Event?> GetByIdAsync(int id);
        Task AddAsync(Event evt);
        Task<bool> UpdateAsync(Event evt);
        Task<bool> DeleteAsync(int id);
    }
}

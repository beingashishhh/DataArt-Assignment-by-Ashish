using MyProject.Domain.Entities;

namespace MyProject.Domain.Interfaces
{
    public interface ICalendarRepository
    {
        Task<IEnumerable<Calendar>> GetAllAsync();
        Task<Calendar?> GetByIdAsync(int id);
        Task AddAsync(Calendar calendar);
        Task<bool> UpdateAsync(Calendar calendar);
        Task<bool> DeleteAsync(int id);
    }
}

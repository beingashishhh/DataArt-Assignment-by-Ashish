using MyProject.Domain.Entities;

namespace MyProject.Domain.Interfaces
{
    public interface IMeetingRepository
    {
        Task<IEnumerable<Meeting>> GetAllAsync();
        Task<Meeting?> GetByIdAsync(int id);
        Task<Meeting> AddAsync(Meeting meeting);
        Task<bool> UpdateAsync(Meeting meeting);
        Task<bool> DeleteAsync(int id);
    }
}

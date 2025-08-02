using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Repositories
{
    public interface IRiskRepository
    {
        Task<IEnumerable<Risk>> GetAllAsync();
        Task<Risk?> GetByIdAsync(Guid id);
        Task<IEnumerable<Risk>> GetByProjectIdAsync(Guid projectId);
        Task<IEnumerable<Risk>> GetByStatusAsync(string status);
        Task<IEnumerable<Risk>> GetByCategoryAsync(string category);
        Task<IEnumerable<Risk>> GetByOwnerIdAsync(Guid ownerId);
        Task<Risk> AddAsync(Risk risk);
        Task<Risk> UpdateAsync(Risk risk);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<IEnumerable<Risk>> GetHighRiskRisksAsync();
    }
} 
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
        Task<IEnumerable<Risk>> GetByTenantIdAsync(Guid tenantId);
        Task<IEnumerable<Risk>> GetByTenantAndStatusAsync(Guid tenantId, string status);
        Task<IEnumerable<Risk>> GetByTenantAndCategoryAsync(Guid tenantId, string category);
        Task<IEnumerable<Risk>> GetByProjectAndStatusAsync(Guid projectId, string status);
        Task<IEnumerable<Risk>> GetByTenantAndOwnerAsync(Guid tenantId, Guid ownerId);
        Task<IEnumerable<Risk>> GetOverdueRisksAsync();
        Task<IEnumerable<Risk>> GetByExposureRangeAsync(int minExposure, int maxExposure);
    }
} 
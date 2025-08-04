using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Repositories
{
    public interface IMilestoneRepository
    {
        Task<IEnumerable<Milestone>> GetAllAsync();
        Task<Milestone?> GetByIdAsync(Guid id);
        Task<IEnumerable<Milestone>> GetByProjectIdAsync(Guid projectId);
        Task<IEnumerable<Milestone>> GetByTenantIdAsync(Guid tenantId);
        Task<IEnumerable<Milestone>> GetAchievedMilestonesAsync();
        Task<IEnumerable<Milestone>> GetPendingMilestonesAsync();
        Task<IEnumerable<Milestone>> GetOverdueMilestonesAsync();
        Task<Milestone> AddAsync(Milestone milestone);
        Task<Milestone> UpdateAsync(Milestone milestone);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> ExistsByNameInProjectAsync(Guid projectId, string milestoneName, Guid? excludeMilestoneId = null);
    }
} 
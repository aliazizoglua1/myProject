using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Repositories
{
    public interface IQualityAssuranceRepository
    {
        Task<IEnumerable<QualityAssurance>> GetAllAsync();
        Task<QualityAssurance?> GetByIdAsync(Guid id);
        Task<IEnumerable<QualityAssurance>> GetByProjectIdAsync(Guid projectId);
        Task<IEnumerable<QualityAssurance>> GetByTaskIdAsync(Guid taskId);
        Task<IEnumerable<QualityAssurance>> GetByStatusAsync(string status);
        Task<IEnumerable<QualityAssurance>> GetByItemTypeAsync(string itemType);
        Task<IEnumerable<QualityAssurance>> GetBySeverityAsync(string severity);
        Task<IEnumerable<QualityAssurance>> GetByPriorityAsync(string priority);
        Task<IEnumerable<QualityAssurance>> GetByReportedByUserIdAsync(Guid userId);
        Task<IEnumerable<QualityAssurance>> GetByAssignedToUserIdAsync(Guid userId);
        Task<IEnumerable<QualityAssurance>> GetOpenItemsAsync();
        Task<IEnumerable<QualityAssurance>> GetClosedItemsAsync();
        Task<IEnumerable<QualityAssurance>> GetByReportedDateRangeAsync(DateOnly startDate, DateOnly endDate);
        Task<IEnumerable<QualityAssurance>> GetByTestEnvironmentAsync(string testEnvironment);
        Task<QualityAssurance> AddAsync(QualityAssurance qualityAssurance);
        Task<QualityAssurance> UpdateAsync(QualityAssurance qualityAssurance);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<IEnumerable<QualityAssurance>> SearchBySummaryAsync(string searchTerm);
    }
} 
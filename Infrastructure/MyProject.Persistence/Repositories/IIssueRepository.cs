using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Repositories
{
    public interface IIssueRepository
    {
        Task<IEnumerable<Issue>> GetAllAsync();
        Task<Issue?> GetByIdAsync(Guid id);
        Task<IEnumerable<Issue>> GetByProjectIdAsync(Guid projectId);
        Task<IEnumerable<Issue>> GetByTaskIdAsync(Guid taskId);
        Task<IEnumerable<Issue>> GetByStatusAsync(string status);
        Task<IEnumerable<Issue>> GetByPriorityAsync(string priority);
        Task<IEnumerable<Issue>> GetBySeverityAsync(string severity);
        Task<IEnumerable<Issue>> GetByAssignedUserIdAsync(Guid userId);
        Task<IEnumerable<Issue>> GetByIssueTypeAsync(string issueType);
        Task<IEnumerable<Issue>> GetOpenIssuesAsync();
        Task<IEnumerable<Issue>> GetCriticalIssuesAsync();
        Task<IEnumerable<Issue>> GetOverdueIssuesAsync();
        Task<IEnumerable<Issue>> GetByTenantIdAsync(Guid tenantId);
        Task<IEnumerable<Issue>> GetByMilestoneIdAsync(Guid milestoneId);
        Task<IEnumerable<Issue>> GetByTenantAndStatusAsync(Guid tenantId, string status);
        Task<IEnumerable<Issue>> GetByProjectAndStatusAsync(Guid projectId, string status);
        Task<IEnumerable<Issue>> GetByTenantAndPriorityAsync(Guid tenantId, string priority);
        Task<IEnumerable<Issue>> GetByTenantAndSeverityAsync(Guid tenantId, string severity);
        Task<Issue> AddAsync(Issue issue);
        Task<Issue> UpdateAsync(Issue issue);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> ExistsByProjectAndNameAsync(Guid projectId, string issueName, Guid? excludeIssueId = null);
        Task<IEnumerable<Issue>> SearchByDescriptionAsync(string searchTerm);
    }
} 
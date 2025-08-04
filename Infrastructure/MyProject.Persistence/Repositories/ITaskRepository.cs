using MyProject.Persistence.Entities;
using Task = MyProject.Persistence.Entities.Task;
namespace MyProject.Persistence.Repositories
{
    public interface ITaskRepository
    {
        Task<IEnumerable<Task>> GetAllAsync();
        Task<Task?> GetByIdAsync(Guid id);
        Task<IEnumerable<Task>> GetByProjectIdAsync(Guid projectId);
        Task<IEnumerable<Task>> GetByStatusAsync(string status);
        Task<IEnumerable<Task>> GetByPriorityAsync(string priority);
        Task<IEnumerable<Task>> GetByAssignedUserIdAsync(Guid userId);
        Task<IEnumerable<Task>> GetByParentTaskIdAsync(Guid parentTaskId);
        Task<IEnumerable<Task>> GetMilestonesAsync();
        Task<IEnumerable<Task>> GetOverdueTasksAsync();
        Task<Task> AddAsync(Task task);
        Task<Task> UpdateAsync(Task task);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<IEnumerable<Task>> GetTasksWithSubtasksAsync();
        Task<IEnumerable<Task>> GetByTenantIdAsync(Guid tenantId);
        Task<IEnumerable<Task>> GetByTenantAndStatusAsync(Guid tenantId, string status);
        Task<IEnumerable<Task>> GetByTenantAndPriorityAsync(Guid tenantId, string priority);
        Task<IEnumerable<Task>> GetByProjectAndStatusAsync(Guid projectId, string status);
        Task<IEnumerable<Task>> GetByMilestoneIdAsync(Guid milestoneId);
        Task<IEnumerable<Task>> GetByIssueIdAsync(Guid issueId);
        Task<IEnumerable<Task>> GetByTenantAndUserAsync(Guid tenantId, Guid userId);
        Task<IEnumerable<Task>> GetOverdueTasksByTenantAsync(Guid tenantId);
        Task<IEnumerable<Task>> GetMilestonesByTenantAsync(Guid tenantId);
    }
} 
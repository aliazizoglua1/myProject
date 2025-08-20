using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Repositories;

public interface ITaskHistoryRepository
{
    Task<IEnumerable<TaskHistory>> GetAllAsync();
    Task<TaskHistory?> GetByIdAsync(Guid taskHistoryId);
    Task<IEnumerable<TaskHistory>> GetByTaskIdAsync(Guid taskId);
    Task<IEnumerable<TaskHistory>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<TaskHistory>> GetByTenantIdAsync(Guid tenantId);
    Task<TaskHistory> CreateAsync(TaskHistory taskHistory);
    Task<TaskHistory> UpdateAsync(TaskHistory taskHistory);
    Task<bool> DeleteAsync(Guid taskHistoryId);
    Task<bool> DeleteByTaskIdAsync(Guid taskId);
} 
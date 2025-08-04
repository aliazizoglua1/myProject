using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Repositories
{
    public interface ITaskTimeLogRepository
    {
        Task<TaskTimeLog?> GetByIdAsync(Guid id);
        Task<IEnumerable<TaskTimeLog>> GetAllAsync();
        Task<IEnumerable<TaskTimeLog>> GetByTaskIdAsync(Guid taskId);
        Task<IEnumerable<TaskTimeLog>> GetByResourceIdAsync(Guid resourceId);
        Task<IEnumerable<TaskTimeLog>> GetByTenantIdAsync(Guid tenantId);
        Task<IEnumerable<TaskTimeLog>> GetByDateRangeAsync(DateOnly startDate, DateOnly endDate);
        Task<IEnumerable<TaskTimeLog>> GetByTaskAndDateRangeAsync(Guid taskId, DateOnly startDate, DateOnly endDate);
        Task<IEnumerable<TaskTimeLog>> GetByResourceAndDateRangeAsync(Guid resourceId, DateOnly startDate, DateOnly endDate);
        Task<IEnumerable<TaskTimeLog>> GetByTenantAndDateRangeAsync(Guid tenantId, DateOnly startDate, DateOnly endDate);
        Task<decimal> GetTotalHoursByTaskAsync(Guid taskId);
        Task<decimal> GetTotalHoursByResourceAsync(Guid resourceId);
        Task<decimal> GetTotalHoursByTaskAndDateRangeAsync(Guid taskId, DateOnly startDate, DateOnly endDate);
        Task<decimal> GetTotalHoursByResourceAndDateRangeAsync(Guid resourceId, DateOnly startDate, DateOnly endDate);
        Task<decimal> GetTotalBillableHoursByTaskAsync(Guid taskId);
        Task<decimal> GetTotalBillableHoursByResourceAsync(Guid resourceId);
        Task<decimal> GetTotalCostByTaskAsync(Guid taskId);
        Task<decimal> GetTotalCostByResourceAsync(Guid resourceId);
        Task<TaskTimeLog> CreateAsync(TaskTimeLog taskTimeLog);
        Task<TaskTimeLog> UpdateAsync(TaskTimeLog taskTimeLog);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
} 
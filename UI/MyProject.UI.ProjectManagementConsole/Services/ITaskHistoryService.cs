using MyProject.UI.ProjectManagementConsole.Models;

namespace MyProject.UI.ProjectManagementConsole.Services;

public interface ITaskHistoryService
{
    Task<IEnumerable<TaskHistoryDto>> GetTaskHistoriesAsync();
    Task<TaskHistoryDto?> GetTaskHistoryAsync(Guid taskHistoryId);
    Task<IEnumerable<TaskHistoryDto>> GetByTaskIdAsync(Guid taskId);
    Task<IEnumerable<TaskHistoryDto>> GetByUserIdAsync(Guid userId);
    Task<IEnumerable<TaskHistoryDto>> GetByTenantIdAsync(Guid tenantId);
    Task<TaskHistoryDto> CreateTaskHistoryAsync(CreateTaskHistoryDto createDto);
    Task<TaskHistoryDto> UpdateTaskHistoryAsync(Guid taskHistoryId, UpdateTaskHistoryDto updateDto);
    Task<bool> DeleteTaskHistoryAsync(Guid taskHistoryId);
    Task<bool> DeleteByTaskIdAsync(Guid taskId);
} 
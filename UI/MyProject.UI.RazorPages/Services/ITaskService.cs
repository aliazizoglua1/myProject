using MyProject.UI.RazorPages.Models;

namespace MyProject.UI.RazorPages.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllTasksAsync();
        Task<TaskDto?> GetTaskAsync(Guid id);
        Task<TaskDto> CreateTaskAsync(CreateTaskDto task);
        Task<TaskDto> UpdateTaskAsync(Guid id, UpdateTaskDto task);
        Task<bool> DeleteTaskAsync(Guid id);
        Task<IEnumerable<TaskDto>> GetTasksByProjectAsync(Guid projectId);
        Task<IEnumerable<TaskDto>> GetTasksByStatusAsync(string status);
        Task<IEnumerable<TaskDto>> GetTasksByPriorityAsync(string priority);
        Task<IEnumerable<TaskDto>> GetTasksByAssignedUserAsync(Guid userId);
        Task<IEnumerable<TaskDto>> GetSubtasksAsync(Guid parentTaskId);
        Task<IEnumerable<TaskDto>> GetMilestonesAsync();
        Task<IEnumerable<TaskDto>> GetOverdueTasksAsync();
        Task<IEnumerable<TaskDto>> GetTasksWithSubtasksAsync();
    }
}
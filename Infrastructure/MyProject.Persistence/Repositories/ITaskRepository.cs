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
    }
} 
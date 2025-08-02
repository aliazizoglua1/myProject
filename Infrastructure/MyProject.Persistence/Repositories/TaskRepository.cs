using Microsoft.EntityFrameworkCore;
using MyProject.Persistence.Entities;
using Task = MyProject.Persistence.Entities.Task;
namespace MyProject.Persistence.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Task>> GetAllAsync()
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.ParentTask)
                .Include(t => t.Subtasks)
                .ToListAsync();
        }

        public async Task<Task?> GetByIdAsync(Guid id)
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.ParentTask)
                .Include(t => t.Subtasks)
                .FirstOrDefaultAsync(t => t.TaskId == id);
        }

        public async Task<IEnumerable<Task>> GetByProjectIdAsync(Guid projectId)
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.ParentTask)
                .Include(t => t.Subtasks)
                .Where(t => t.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Task>> GetByStatusAsync(string status)
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.ParentTask)
                .Include(t => t.Subtasks)
                .Where(t => t.TaskStatus == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Task>> GetByPriorityAsync(string priority)
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.ParentTask)
                .Include(t => t.Subtasks)
                .Where(t => t.Priority == priority)
                .ToListAsync();
        }

        public async Task<IEnumerable<Task>> GetByAssignedUserIdAsync(Guid userId)
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.ParentTask)
                .Include(t => t.Subtasks)
                .Where(t => t.AssignedToUserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Task>> GetByParentTaskIdAsync(Guid parentTaskId)
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.ParentTask)
                .Include(t => t.Subtasks)
                .Where(t => t.ParentTaskId == parentTaskId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Task>> GetMilestonesAsync()
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.ParentTask)
                .Include(t => t.Subtasks)
                .Where(t => t.IsMilestone)
                .ToListAsync();
        }

        public async Task<IEnumerable<Task>> GetOverdueTasksAsync()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            return await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.ParentTask)
                .Include(t => t.Subtasks)
                .Where(t => t.PlannedEndDate.HasValue && t.PlannedEndDate < today && t.TaskStatus != "Completed")
                .ToListAsync();
        }

        public async Task<Task> AddAsync(Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Task> UpdateAsync(Task task)
        {
            task.UpdatedAt = DateTime.UtcNow;
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Tasks.AnyAsync(t => t.TaskId == id);
        }

        public async Task<IEnumerable<Task>> GetTasksWithSubtasksAsync()
        {
            return await _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.ParentTask)
                .Include(t => t.Subtasks)
                .Where(t => t.Subtasks != null && t.Subtasks.Any())
                .ToListAsync();
        }
    }
} 
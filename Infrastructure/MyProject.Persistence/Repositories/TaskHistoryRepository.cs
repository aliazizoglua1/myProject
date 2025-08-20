using Microsoft.EntityFrameworkCore;
using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Repositories;

public class TaskHistoryRepository : ITaskHistoryRepository
{
    private readonly ApplicationDbContext _context;

    public TaskHistoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskHistory>> GetAllAsync()
    {
        return await _context.TaskHistories
            .Include(th => th.Task)
            .Include(th => th.User)
            .Include(th => th.Organization)
            .OrderByDescending(th => th.CreatedAt)
            .ToListAsync();
    }

    public async Task<TaskHistory?> GetByIdAsync(Guid taskHistoryId)
    {
        return await _context.TaskHistories
            .Include(th => th.Task)
            .Include(th => th.User)
            .Include(th => th.Organization)
            .FirstOrDefaultAsync(th => th.TaskHistoryId == taskHistoryId);
    }

    public async Task<IEnumerable<TaskHistory>> GetByTaskIdAsync(Guid taskId)
    {
        return await _context.TaskHistories
            .Include(th => th.Task)
            .Include(th => th.User)
            .Include(th => th.Organization)
            .Where(th => th.TaskId == taskId)
            .OrderByDescending(th => th.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<TaskHistory>> GetByUserIdAsync(Guid userId)
    {
        return await _context.TaskHistories
            .Include(th => th.Task)
            .Include(th => th.User)
            .Include(th => th.Organization)
            .Where(th => th.UserId == userId)
            .OrderByDescending(th => th.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<TaskHistory>> GetByTenantIdAsync(Guid organizationId)
    {
        return await _context.TaskHistories
            .Include(th => th.Task)
            .Include(th => th.User)
            .Include(th => th.Organization)
            .Where(th => th.OrganizationId == organizationId)
            .OrderByDescending(th => th.CreatedAt)
            .ToListAsync();
    }

    public async Task<TaskHistory> CreateAsync(TaskHistory taskHistory)
    {
        taskHistory.CreatedAt = DateTime.UtcNow;
        //taskHistory.UpdatedAt = DateTime.UtcNow;
        
        _context.TaskHistories.Add(taskHistory);
        await _context.SaveChangesAsync();
        
        return taskHistory;
    }

    public async Task<TaskHistory> UpdateAsync(TaskHistory taskHistory)
    {
       // taskHistory.UpdatedAt = DateTime.UtcNow;
        
        _context.TaskHistories.Update(taskHistory);
        await _context.SaveChangesAsync();
        
        return taskHistory;
    }

    public async Task<bool> DeleteAsync(Guid taskHistoryId)
    {
        var taskHistory = await _context.TaskHistories.FindAsync(taskHistoryId);
        if (taskHistory == null)
            return false;

        _context.TaskHistories.Remove(taskHistory);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> DeleteByTaskIdAsync(Guid taskId)
    {
        var taskHistories = await _context.TaskHistories
            .Where(th => th.TaskId == taskId)
            .ToListAsync();

        if (!taskHistories.Any())
            return false;

        _context.TaskHistories.RemoveRange(taskHistories);
        await _context.SaveChangesAsync();
        
        return true;
    }
} 
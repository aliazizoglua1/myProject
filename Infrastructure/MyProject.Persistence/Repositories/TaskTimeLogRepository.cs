using Microsoft.EntityFrameworkCore;
using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Repositories
{
    public class TaskTimeLogRepository : ITaskTimeLogRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskTimeLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaskTimeLog?> GetByIdAsync(Guid id)
        {
            return await _context.TaskTimeLogs
                .Include(t => t.Task)
                .Include(t => t.Resource)
                .Include(t => t.Tenant)
                .FirstOrDefaultAsync(t => t.TaskTimeLogId == id);
        }

        public async Task<IEnumerable<TaskTimeLog>> GetAllAsync()
        {
            return await _context.TaskTimeLogs
                .Include(t => t.Task)
                .Include(t => t.Resource)
                .Include(t => t.Tenant)
                .OrderByDescending(t => t.DateWorked)
                .ThenByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskTimeLog>> GetByTaskIdAsync(Guid taskId)
        {
            return await _context.TaskTimeLogs
                .Include(t => t.Task)
                .Include(t => t.Resource)
                .Include(t => t.Tenant)
                .Where(t => t.TaskId == taskId)
                .OrderByDescending(t => t.DateWorked)
                .ThenByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskTimeLog>> GetByResourceIdAsync(Guid resourceId)
        {
            return await _context.TaskTimeLogs
                .Include(t => t.Task)
                .Include(t => t.Resource)
                .Include(t => t.Tenant)
                .Where(t => t.ResourceId == resourceId)
                .OrderByDescending(t => t.DateWorked)
                .ThenByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskTimeLog>> GetByTenantIdAsync(Guid tenantId)
        {
            return await _context.TaskTimeLogs
                .Include(t => t.Task)
                .Include(t => t.Resource)
                .Include(t => t.Tenant)
                .Where(t => t.TenantId == tenantId)
                .OrderByDescending(t => t.DateWorked)
                .ThenByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskTimeLog>> GetByDateRangeAsync(DateOnly startDate, DateOnly endDate)
        {
            return await _context.TaskTimeLogs
                .Include(t => t.Task)
                .Include(t => t.Resource)
                .Include(t => t.Tenant)
                .Where(t => t.DateWorked >= startDate && t.DateWorked <= endDate)
                .OrderByDescending(t => t.DateWorked)
                .ThenByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskTimeLog>> GetByTaskAndDateRangeAsync(Guid taskId, DateOnly startDate, DateOnly endDate)
        {
            return await _context.TaskTimeLogs
                .Include(t => t.Task)
                .Include(t => t.Resource)
                .Include(t => t.Tenant)
                .Where(t => t.TaskId == taskId && t.DateWorked >= startDate && t.DateWorked <= endDate)
                .OrderByDescending(t => t.DateWorked)
                .ThenByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskTimeLog>> GetByResourceAndDateRangeAsync(Guid resourceId, DateOnly startDate, DateOnly endDate)
        {
            return await _context.TaskTimeLogs
                .Include(t => t.Task)
                .Include(t => t.Resource)
                .Include(t => t.Tenant)
                .Where(t => t.ResourceId == resourceId && t.DateWorked >= startDate && t.DateWorked <= endDate)
                .OrderByDescending(t => t.DateWorked)
                .ThenByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskTimeLog>> GetByTenantAndDateRangeAsync(Guid tenantId, DateOnly startDate, DateOnly endDate)
        {
            return await _context.TaskTimeLogs
                .Include(t => t.Task)
                .Include(t => t.Resource)
                .Include(t => t.Tenant)
                .Where(t => t.TenantId == tenantId && t.DateWorked >= startDate && t.DateWorked <= endDate)
                .OrderByDescending(t => t.DateWorked)
                .ThenByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<decimal> GetTotalHoursByTaskAsync(Guid taskId)
        {
            return await _context.TaskTimeLogs
                .Where(t => t.TaskId == taskId)
                .SumAsync(t => t.HoursWorked);
        }

        public async Task<decimal> GetTotalHoursByResourceAsync(Guid resourceId)
        {
            return await _context.TaskTimeLogs
                .Where(t => t.ResourceId == resourceId)
                .SumAsync(t => t.HoursWorked);
        }

        public async Task<decimal> GetTotalHoursByTaskAndDateRangeAsync(Guid taskId, DateOnly startDate, DateOnly endDate)
        {
            return await _context.TaskTimeLogs
                .Where(t => t.TaskId == taskId && t.DateWorked >= startDate && t.DateWorked <= endDate)
                .SumAsync(t => t.HoursWorked);
        }

        public async Task<decimal> GetTotalHoursByResourceAndDateRangeAsync(Guid resourceId, DateOnly startDate, DateOnly endDate)
        {
            return await _context.TaskTimeLogs
                .Where(t => t.ResourceId == resourceId && t.DateWorked >= startDate && t.DateWorked <= endDate)
                .SumAsync(t => t.HoursWorked);
        }

        public async Task<decimal> GetTotalBillableHoursByTaskAsync(Guid taskId)
        {
            return await _context.TaskTimeLogs
                .Where(t => t.TaskId == taskId && t.IsBillable)
                .SumAsync(t => t.HoursWorked);
        }

        public async Task<decimal> GetTotalBillableHoursByResourceAsync(Guid resourceId)
        {
            return await _context.TaskTimeLogs
                .Where(t => t.ResourceId == resourceId && t.IsBillable)
                .SumAsync(t => t.HoursWorked);
        }

        public async Task<decimal> GetTotalCostByTaskAsync(Guid taskId)
        {
            return await _context.TaskTimeLogs
                .Where(t => t.TaskId == taskId && t.HourlyRate.HasValue)
                .SumAsync(t => t.HoursWorked * t.HourlyRate.Value);
        }

        public async Task<decimal> GetTotalCostByResourceAsync(Guid resourceId)
        {
            return await _context.TaskTimeLogs
                .Where(t => t.ResourceId == resourceId && t.HourlyRate.HasValue)
                .SumAsync(t => t.HoursWorked * t.HourlyRate.Value);
        }

        public async Task<TaskTimeLog> CreateAsync(TaskTimeLog taskTimeLog)
        {
            taskTimeLog.CreatedAt = DateTime.UtcNow;
            taskTimeLog.UpdatedAt = DateTime.UtcNow;
            
            _context.TaskTimeLogs.Add(taskTimeLog);
            await _context.SaveChangesAsync();
            
            return taskTimeLog;
        }

        public async Task<TaskTimeLog> UpdateAsync(TaskTimeLog taskTimeLog)
        {
            taskTimeLog.UpdatedAt = DateTime.UtcNow;
            
            _context.TaskTimeLogs.Update(taskTimeLog);
            await _context.SaveChangesAsync();
            
            return taskTimeLog;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var taskTimeLog = await _context.TaskTimeLogs.FindAsync(id);
            if (taskTimeLog == null)
                return false;

            _context.TaskTimeLogs.Remove(taskTimeLog);
            await _context.SaveChangesAsync();
            
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.TaskTimeLogs.AnyAsync(t => t.TaskTimeLogId == id);
        }
    }
} 
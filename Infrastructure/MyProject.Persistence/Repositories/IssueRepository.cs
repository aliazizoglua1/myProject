using Microsoft.EntityFrameworkCore;
using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Repositories
{
    public class IssueRepository : IIssueRepository
    {
        private readonly ApplicationDbContext _context;

        public IssueRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Issue>> GetAllAsync()
        {
            return await _context.Issues
                .Include(i => i.Project)
                .Include(i => i.Task)
                .ToListAsync();
        }

        public async Task<Issue?> GetByIdAsync(Guid id)
        {
            return await _context.Issues
                .Include(i => i.Project)
                .Include(i => i.Task)
                .FirstOrDefaultAsync(i => i.IssueId == id);
        }

        public async Task<IEnumerable<Issue>> GetByProjectIdAsync(Guid projectId)
        {
            return await _context.Issues
                .Include(i => i.Project)
                .Include(i => i.Task)
                .Where(i => i.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Issue>> GetByTaskIdAsync(Guid taskId)
        {
            return await _context.Issues
                .Include(i => i.Project)
                .Include(i => i.Task)
                .Where(i => i.TaskId == taskId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Issue>> GetByStatusAsync(string status)
        {
            return await _context.Issues
                .Include(i => i.Project)
                .Include(i => i.Task)
                .Where(i => i.Status == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Issue>> GetByPriorityAsync(string priority)
        {
            return await _context.Issues
                .Include(i => i.Project)
                .Include(i => i.Task)
                .Where(i => i.Priority == priority)
                .ToListAsync();
        }

        public async Task<IEnumerable<Issue>> GetBySeverityAsync(string severity)
        {
            return await _context.Issues
                .Include(i => i.Project)
                .Include(i => i.Task)
                .Where(i => i.Severity == severity)
                .ToListAsync();
        }

        public async Task<IEnumerable<Issue>> GetByAssignedUserIdAsync(Guid userId)
        {
            return await _context.Issues
                .Include(i => i.Project)
                .Include(i => i.Task)
                .Where(i => i.AssignedToUserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Issue>> GetByIssueTypeAsync(string issueType)
        {
            return await _context.Issues
                .Include(i => i.Project)
                .Include(i => i.Task)
                .Where(i => i.IssueType == issueType)
                .ToListAsync();
        }

        public async Task<IEnumerable<Issue>> GetOpenIssuesAsync()
        {
            return await _context.Issues
                .Include(i => i.Project)
                .Include(i => i.Task)
                .Where(i => i.Status == "Open")
                .ToListAsync();
        }

        public async Task<IEnumerable<Issue>> GetCriticalIssuesAsync()
        {
            return await _context.Issues
                .Include(i => i.Project)
                .Include(i => i.Task)
                .Where(i => i.Severity == "Critical" || i.Priority == "Immediate")
                .ToListAsync();
        }

        public async Task<IEnumerable<Issue>> GetOverdueIssuesAsync()
        {
            var thirtyDaysAgo = DateOnly.FromDateTime(DateTime.Today.AddDays(-30));
            return await _context.Issues
                .Include(i => i.Project)
                .Include(i => i.Task)
                .Where(i => i.Status == "Open" && i.OpenedDate < thirtyDaysAgo)
                .ToListAsync();
        }

        public async Task<Issue> AddAsync(Issue issue)
        {
            _context.Issues.Add(issue);
            await _context.SaveChangesAsync();
            return issue;
        }

        public async Task<Issue> UpdateAsync(Issue issue)
        {
            issue.UpdatedAt = DateTime.UtcNow;
            _context.Issues.Update(issue);
            await _context.SaveChangesAsync();
            return issue;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
                return false;

            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Issues.AnyAsync(i => i.IssueId == id);
        }

        public async Task<IEnumerable<Issue>> SearchByDescriptionAsync(string searchTerm)
        {
            return await _context.Issues
                .Include(i => i.Project)
                .Include(i => i.Task)
                .Where(i => i.IssueDescription.Contains(searchTerm) || 
                           i.IssueName.Contains(searchTerm) ||
                           i.RootCause.Contains(searchTerm))
                .ToListAsync();
        }
    }
} 
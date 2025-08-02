using Microsoft.EntityFrameworkCore;
using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Repositories
{
    public class QualityAssuranceRepository : IQualityAssuranceRepository
    {
        private readonly ApplicationDbContext _context;

        public QualityAssuranceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<QualityAssurance>> GetAllAsync()
        {
            return await _context.QualityAssurances
                .Include(qa => qa.Project)
                .Include(qa => qa.Task)
                .ToListAsync();
        }

        public async Task<QualityAssurance?> GetByIdAsync(Guid id)
        {
            return await _context.QualityAssurances
                .Include(qa => qa.Project)
                .Include(qa => qa.Task)
                .FirstOrDefaultAsync(qa => qa.QaItemId == id);
        }

        public async Task<IEnumerable<QualityAssurance>> GetByProjectIdAsync(Guid projectId)
        {
            return await _context.QualityAssurances
                .Include(qa => qa.Project)
                .Include(qa => qa.Task)
                .Where(qa => qa.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<IEnumerable<QualityAssurance>> GetByTaskIdAsync(Guid taskId)
        {
            return await _context.QualityAssurances
                .Include(qa => qa.Project)
                .Include(qa => qa.Task)
                .Where(qa => qa.TaskId == taskId)
                .ToListAsync();
        }

        public async Task<IEnumerable<QualityAssurance>> GetByStatusAsync(string status)
        {
            return await _context.QualityAssurances
                .Include(qa => qa.Project)
                .Include(qa => qa.Task)
                .Where(qa => qa.Status == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<QualityAssurance>> GetByItemTypeAsync(string itemType)
        {
            return await _context.QualityAssurances
                .Include(qa => qa.Project)
                .Include(qa => qa.Task)
                .Where(qa => qa.ItemType == itemType)
                .ToListAsync();
        }

        public async Task<IEnumerable<QualityAssurance>> GetBySeverityAsync(string severity)
        {
            return await _context.QualityAssurances
                .Include(qa => qa.Project)
                .Include(qa => qa.Task)
                .Where(qa => qa.Severity == severity)
                .ToListAsync();
        }

        public async Task<IEnumerable<QualityAssurance>> GetByPriorityAsync(string priority)
        {
            return await _context.QualityAssurances
                .Include(qa => qa.Project)
                .Include(qa => qa.Task)
                .Where(qa => qa.Priority == priority)
                .ToListAsync();
        }

        public async Task<IEnumerable<QualityAssurance>> GetByReportedByUserIdAsync(Guid userId)
        {
            return await _context.QualityAssurances
                .Include(qa => qa.Project)
                .Include(qa => qa.Task)
                .Where(qa => qa.ReportedByUserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<QualityAssurance>> GetByAssignedToUserIdAsync(Guid userId)
        {
            return await _context.QualityAssurances
                .Include(qa => qa.Project)
                .Include(qa => qa.Task)
                .Where(qa => qa.AssignedToUserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<QualityAssurance>> GetOpenItemsAsync()
        {
            return await _context.QualityAssurances
                .Include(qa => qa.Project)
                .Include(qa => qa.Task)
                .Where(qa => qa.Status == "Open" || qa.Status == "In Progress")
                .ToListAsync();
        }

        public async Task<IEnumerable<QualityAssurance>> GetClosedItemsAsync()
        {
            return await _context.QualityAssurances
                .Include(qa => qa.Project)
                .Include(qa => qa.Task)
                .Where(qa => qa.Status == "Closed" || qa.Status == "Verified" || qa.Status == "Rejected")
                .ToListAsync();
        }

        public async Task<IEnumerable<QualityAssurance>> GetByReportedDateRangeAsync(DateOnly startDate, DateOnly endDate)
        {
            return await _context.QualityAssurances
                .Include(qa => qa.Project)
                .Include(qa => qa.Task)
                .Where(qa => qa.ReportedDate >= startDate && qa.ReportedDate <= endDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<QualityAssurance>> GetByTestEnvironmentAsync(string testEnvironment)
        {
            return await _context.QualityAssurances
                .Include(qa => qa.Project)
                .Include(qa => qa.Task)
                .Where(qa => qa.TestEnvironment == testEnvironment)
                .ToListAsync();
        }

        public async Task<QualityAssurance> AddAsync(QualityAssurance qualityAssurance)
        {
            _context.QualityAssurances.Add(qualityAssurance);
            await _context.SaveChangesAsync();
            return qualityAssurance;
        }

        public async Task<QualityAssurance> UpdateAsync(QualityAssurance qualityAssurance)
        {
            qualityAssurance.UpdatedAt = DateTime.UtcNow;
            _context.QualityAssurances.Update(qualityAssurance);
            await _context.SaveChangesAsync();
            return qualityAssurance;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var qualityAssurance = await _context.QualityAssurances.FindAsync(id);
            if (qualityAssurance == null)
                return false;

            _context.QualityAssurances.Remove(qualityAssurance);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.QualityAssurances.AnyAsync(qa => qa.QaItemId == id);
        }

        public async Task<IEnumerable<QualityAssurance>> SearchBySummaryAsync(string searchTerm)
        {
            return await _context.QualityAssurances
                .Include(qa => qa.Project)
                .Include(qa => qa.Task)
                .Where(qa => qa.Summary.Contains(searchTerm) || 
                           qa.Description.Contains(searchTerm) ||
                           qa.ResolutionNotes.Contains(searchTerm))
                .ToListAsync();
        }
    }
} 
using Microsoft.EntityFrameworkCore;
using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Repositories
{
    public class MilestoneRepository : IMilestoneRepository
    {
        private readonly ApplicationDbContext _context;

        public MilestoneRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Milestone>> GetAllAsync()
        {
            return await _context.Milestones.ToListAsync();
        }

        public async Task<Milestone?> GetByIdAsync(Guid id)
        {
            return await _context.Milestones.FindAsync(id);
        }

        public async Task<IEnumerable<Milestone>> GetByProjectIdAsync(Guid projectId)
        {
            return await _context.Milestones
                .Where(m => m.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Milestone>> GetByTenantIdAsync(Guid tenantId)
        {
            return await _context.Milestones
                .Where(m => m.TenantId == tenantId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Milestone>> GetAchievedMilestonesAsync()
        {
            return await _context.Milestones
                .Where(m => m.IsAchieved)
                .ToListAsync();
        }

        public async Task<IEnumerable<Milestone>> GetPendingMilestonesAsync()
        {
            return await _context.Milestones
                .Where(m => !m.IsAchieved)
                .ToListAsync();
        }

        public async Task<IEnumerable<Milestone>> GetOverdueMilestonesAsync()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            return await _context.Milestones
                .Where(m => !m.IsAchieved && m.DueDate < today)
                .ToListAsync();
        }

        public async Task<Milestone> AddAsync(Milestone milestone)
        {
            _context.Milestones.Add(milestone);
            await _context.SaveChangesAsync();
            return milestone;
        }

        public async Task<Milestone> UpdateAsync(Milestone milestone)
        {
            milestone.UpdatedAt = DateTime.UtcNow;
            _context.Milestones.Update(milestone);
            await _context.SaveChangesAsync();
            return milestone;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var milestone = await _context.Milestones.FindAsync(id);
            if (milestone == null)
                return false;

            _context.Milestones.Remove(milestone);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Milestones.AnyAsync(m => m.MilestoneId == id);
        }

        public async Task<bool> ExistsByNameInProjectAsync(Guid projectId, string milestoneName, Guid? excludeMilestoneId = null)
        {
            if (excludeMilestoneId.HasValue)
            {
                return await _context.Milestones.AnyAsync(m => 
                    m.ProjectId == projectId && 
                    m.MilestoneName == milestoneName && 
                    m.MilestoneId != excludeMilestoneId.Value);
            }
            return await _context.Milestones.AnyAsync(m => 
                m.ProjectId == projectId && 
                m.MilestoneName == milestoneName);
        }
    }
} 
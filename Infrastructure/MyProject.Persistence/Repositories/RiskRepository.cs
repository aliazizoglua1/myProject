using Microsoft.EntityFrameworkCore;
using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Repositories
{
    public class RiskRepository : IRiskRepository
    {
        private readonly ApplicationDbContext _context;

        public RiskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Risk>> GetAllAsync()
        {
            return await _context.Risks
                .Include(r => r.Project)
                .ToListAsync();
        }

        public async Task<Risk?> GetByIdAsync(Guid id)
        {
            return await _context.Risks
                .Include(r => r.Project)
                .FirstOrDefaultAsync(r => r.RiskId == id);
        }

        public async Task<IEnumerable<Risk>> GetByProjectIdAsync(Guid projectId)
        {
            return await _context.Risks
                .Include(r => r.Project)
                .Where(r => r.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Risk>> GetByStatusAsync(string status)
        {
            return await _context.Risks
                .Include(r => r.Project)
                .Where(r => r.RiskStatus == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Risk>> GetByCategoryAsync(string category)
        {
            return await _context.Risks
                .Include(r => r.Project)
                .Where(r => r.RiskCategory == category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Risk>> GetByOwnerIdAsync(Guid ownerId)
        {
            return await _context.Risks
                .Include(r => r.Project)
                .Where(r => r.RiskOwnerId == ownerId)
                .ToListAsync();
        }

        public async Task<Risk> AddAsync(Risk risk)
        {
            _context.Risks.Add(risk);
            await _context.SaveChangesAsync();
            return risk;
        }

        public async Task<Risk> UpdateAsync(Risk risk)
        {
            risk.UpdatedAt = DateTime.UtcNow;
            _context.Risks.Update(risk);
            await _context.SaveChangesAsync();
            return risk;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var risk = await _context.Risks.FindAsync(id);
            if (risk == null)
                return false;

            _context.Risks.Remove(risk);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Risks.AnyAsync(r => r.RiskId == id);
        }

        public async Task<IEnumerable<Risk>> GetHighRiskRisksAsync()
        {
            return await _context.Risks
                .Include(r => r.Project)
                .Where(r => r.PlannedRiskExposure >= 15 || r.ActualRiskExposure >= 15)
                .ToListAsync();
        }
    }
} 
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
                .Include(r => r.RiskOwner)
                .Include(r => r.Tenant)
                .ToListAsync();
        }

        public async Task<Risk?> GetByIdAsync(Guid id)
        {
            return await _context.Risks
                .Include(r => r.Project)
                .Include(r => r.RiskOwner)
                .Include(r => r.Tenant)
                .FirstOrDefaultAsync(r => r.RiskId == id);
        }

        public async Task<IEnumerable<Risk>> GetByProjectIdAsync(Guid projectId)
        {
            return await _context.Risks
                .Include(r => r.Project)
                .Include(r => r.RiskOwner)
                .Include(r => r.Tenant)
                .Where(r => r.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Risk>> GetByStatusAsync(string status)
        {
            return await _context.Risks
                .Include(r => r.Project)
                .Include(r => r.RiskOwner)
                .Include(r => r.Tenant)
                .Where(r => r.RiskStatus == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Risk>> GetByCategoryAsync(string category)
        {
            return await _context.Risks
                .Include(r => r.Project)
                .Include(r => r.RiskOwner)
                .Include(r => r.Tenant)
                .Where(r => r.RiskCategory == category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Risk>> GetByOwnerIdAsync(Guid ownerId)
        {
            return await _context.Risks
                .Include(r => r.Project)
                .Include(r => r.RiskOwner)
                .Include(r => r.Tenant)
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
                .Include(r => r.RiskOwner)
                .Include(r => r.Tenant)
                .Where(r => r.PlannedRiskExposure >= 15 || r.ActualRiskExposure >= 15)
                .ToListAsync();
        }

        public async Task<IEnumerable<Risk>> GetByTenantIdAsync(Guid tenantId)
        {
            return await _context.Risks
                .Include(r => r.Project)
                .Include(r => r.RiskOwner)
                .Include(r => r.Tenant)
                .Where(r => r.TenantId == tenantId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Risk>> GetByTenantAndStatusAsync(Guid tenantId, string status)
        {
            return await _context.Risks
                .Include(r => r.Project)
                .Include(r => r.RiskOwner)
                .Include(r => r.Tenant)
                .Where(r => r.TenantId == tenantId && r.RiskStatus == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Risk>> GetByTenantAndCategoryAsync(Guid tenantId, string category)
        {
            return await _context.Risks
                .Include(r => r.Project)
                .Include(r => r.RiskOwner)
                .Include(r => r.Tenant)
                .Where(r => r.TenantId == tenantId && r.RiskCategory == category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Risk>> GetByProjectAndStatusAsync(Guid projectId, string status)
        {
            return await _context.Risks
                .Include(r => r.Project)
                .Include(r => r.RiskOwner)
                .Include(r => r.Tenant)
                .Where(r => r.ProjectId == projectId && r.RiskStatus == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Risk>> GetByTenantAndOwnerAsync(Guid tenantId, Guid ownerId)
        {
            return await _context.Risks
                .Include(r => r.Project)
                .Include(r => r.RiskOwner)
                .Include(r => r.Tenant)
                .Where(r => r.TenantId == tenantId && r.RiskOwnerId == ownerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Risk>> GetOverdueRisksAsync()
        {
            var thirtyDaysAgo = DateOnly.FromDateTime(DateTime.Today.AddDays(-30));
            return await _context.Risks
                .Include(r => r.Project)
                .Include(r => r.RiskOwner)
                .Include(r => r.Tenant)
                .Where(r => r.RiskStatus == "Open" && r.LastReviewDate < thirtyDaysAgo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Risk>> GetByExposureRangeAsync(int minExposure, int maxExposure)
        {
            return await _context.Risks
                .Include(r => r.Project)
                .Include(r => r.RiskOwner)
                .Include(r => r.Tenant)
                .Where(r => (r.PlannedRiskExposure >= minExposure && r.PlannedRiskExposure <= maxExposure) ||
                           (r.ActualRiskExposure >= minExposure && r.ActualRiskExposure <= maxExposure))
                .ToListAsync();
        }
    }
} 
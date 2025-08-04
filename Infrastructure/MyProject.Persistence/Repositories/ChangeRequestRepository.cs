using Microsoft.EntityFrameworkCore;
using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Repositories
{
    public class ChangeRequestRepository : IChangeRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public ChangeRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChangeRequest>> GetAllAsync()
        {
            return await _context.ChangeRequests
                .Include(cr => cr.Project)
                .Include(cr => cr.RequestedByUser)
                .Include(cr => cr.ApprovedByUser)
                .Include(cr => cr.Tenant)
                .Include(cr => cr.Milestone)
                .ToListAsync();
        }

        public async Task<ChangeRequest?> GetByIdAsync(Guid id)
        {
            return await _context.ChangeRequests
                .Include(cr => cr.Project)
                .Include(cr => cr.RequestedByUser)
                .Include(cr => cr.ApprovedByUser)
                .Include(cr => cr.Tenant)
                .Include(cr => cr.Milestone)
                .FirstOrDefaultAsync(cr => cr.ChangeRequestId == id);
        }

        public async Task<IEnumerable<ChangeRequest>> GetByProjectIdAsync(Guid projectId)
        {
            return await _context.ChangeRequests
                .Include(cr => cr.Project)
                .Include(cr => cr.RequestedByUser)
                .Include(cr => cr.ApprovedByUser)
                .Include(cr => cr.Tenant)
                .Include(cr => cr.Milestone)
                .Where(cr => cr.ProjectId == projectId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ChangeRequest>> GetByStatusAsync(string status)
        {
            return await _context.ChangeRequests
                .Include(cr => cr.Project)
                .Include(cr => cr.RequestedByUser)
                .Include(cr => cr.ApprovedByUser)
                .Include(cr => cr.Tenant)
                .Include(cr => cr.Milestone)
                .Where(cr => cr.ApprovalStatus == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<ChangeRequest>> GetByRequestedByUserIdAsync(Guid userId)
        {
            return await _context.ChangeRequests
                .Include(cr => cr.Project)
                .Include(cr => cr.RequestedByUser)
                .Include(cr => cr.ApprovedByUser)
                .Include(cr => cr.Tenant)
                .Include(cr => cr.Milestone)
                .Where(cr => cr.RequestedByUserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ChangeRequest>> GetByApprovedByUserIdAsync(Guid userId)
        {
            return await _context.ChangeRequests
                .Include(cr => cr.Project)
                .Include(cr => cr.RequestedByUser)
                .Include(cr => cr.ApprovedByUser)
                .Include(cr => cr.Tenant)
                .Include(cr => cr.Milestone)
                .Where(cr => cr.ApprovedByUserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ChangeRequest>> GetPendingRequestsAsync()
        {
            return await _context.ChangeRequests
                .Include(cr => cr.Project)
                .Include(cr => cr.RequestedByUser)
                .Include(cr => cr.ApprovedByUser)
                .Include(cr => cr.Tenant)
                .Include(cr => cr.Milestone)
                .Where(cr => cr.ApprovalStatus == "Pending")
                .ToListAsync();
        }

        public async Task<IEnumerable<ChangeRequest>> GetApprovedRequestsAsync()
        {
            return await _context.ChangeRequests
                .Include(cr => cr.Project)
                .Include(cr => cr.RequestedByUser)
                .Include(cr => cr.ApprovedByUser)
                .Include(cr => cr.Tenant)
                .Include(cr => cr.Milestone)
                .Where(cr => cr.ApprovalStatus == "Approved")
                .ToListAsync();
        }

        public async Task<IEnumerable<ChangeRequest>> GetRejectedRequestsAsync()
        {
            return await _context.ChangeRequests
                .Include(cr => cr.Project)
                .Include(cr => cr.RequestedByUser)
                .Include(cr => cr.ApprovedByUser)
                .Include(cr => cr.Tenant)
                .Include(cr => cr.Milestone)
                .Where(cr => cr.ApprovalStatus == "Rejected")
                .ToListAsync();
        }

        public async Task<IEnumerable<ChangeRequest>> GetByRequestDateRangeAsync(DateOnly startDate, DateOnly endDate)
        {
            return await _context.ChangeRequests
                .Include(cr => cr.Project)
                .Include(cr => cr.RequestedByUser)
                .Include(cr => cr.ApprovedByUser)
                .Include(cr => cr.Tenant)
                .Include(cr => cr.Milestone)
                .Where(cr => cr.RequestDate >= startDate && cr.RequestDate <= endDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<ChangeRequest>> GetByVersionAffectedAsync(string version)
        {
            return await _context.ChangeRequests
                .Include(cr => cr.Project)
                .Include(cr => cr.RequestedByUser)
                .Include(cr => cr.ApprovedByUser)
                .Include(cr => cr.Tenant)
                .Include(cr => cr.Milestone)
                .Where(cr => cr.VersionAffected == version)
                .ToListAsync();
        }

        public async Task<IEnumerable<ChangeRequest>> GetByTenantIdAsync(Guid tenantId)
        {
            return await _context.ChangeRequests
                .Include(cr => cr.Project)
                .Include(cr => cr.RequestedByUser)
                .Include(cr => cr.ApprovedByUser)
                .Include(cr => cr.Tenant)
                .Include(cr => cr.Milestone)
                .Where(cr => cr.TenantId == tenantId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ChangeRequest>> GetByMilestoneIdAsync(Guid milestoneId)
        {
            return await _context.ChangeRequests
                .Include(cr => cr.Project)
                .Include(cr => cr.RequestedByUser)
                .Include(cr => cr.ApprovedByUser)
                .Include(cr => cr.Tenant)
                .Include(cr => cr.Milestone)
                .Where(cr => cr.MilestoneId == milestoneId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ChangeRequest>> GetByTenantAndStatusAsync(Guid tenantId, string status)
        {
            return await _context.ChangeRequests
                .Include(cr => cr.Project)
                .Include(cr => cr.RequestedByUser)
                .Include(cr => cr.ApprovedByUser)
                .Include(cr => cr.Tenant)
                .Include(cr => cr.Milestone)
                .Where(cr => cr.TenantId == tenantId && cr.ApprovalStatus == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<ChangeRequest>> GetByProjectAndStatusAsync(Guid projectId, string status)
        {
            return await _context.ChangeRequests
                .Include(cr => cr.Project)
                .Include(cr => cr.RequestedByUser)
                .Include(cr => cr.ApprovedByUser)
                .Include(cr => cr.Tenant)
                .Include(cr => cr.Milestone)
                .Where(cr => cr.ProjectId == projectId && cr.ApprovalStatus == status)
                .ToListAsync();
        }

        public async Task<ChangeRequest> AddAsync(ChangeRequest changeRequest)
        {
            _context.ChangeRequests.Add(changeRequest);
            await _context.SaveChangesAsync();
            return changeRequest;
        }

        public async Task<ChangeRequest> UpdateAsync(ChangeRequest changeRequest)
        {
            changeRequest.UpdatedAt = DateTime.UtcNow;
            _context.ChangeRequests.Update(changeRequest);
            await _context.SaveChangesAsync();
            return changeRequest;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var changeRequest = await _context.ChangeRequests.FindAsync(id);
            if (changeRequest == null)
                return false;

            _context.ChangeRequests.Remove(changeRequest);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.ChangeRequests.AnyAsync(cr => cr.ChangeRequestId == id);
        }

        public async Task<bool> ExistsByProjectAndTitleAsync(Guid projectId, string title, Guid? excludeChangeRequestId = null)
        {
            if (excludeChangeRequestId.HasValue)
            {
                return await _context.ChangeRequests.AnyAsync(cr => 
                    cr.ProjectId == projectId && 
                    cr.RequestTitle == title && 
                    cr.ChangeRequestId != excludeChangeRequestId.Value);
            }
            return await _context.ChangeRequests.AnyAsync(cr => 
                cr.ProjectId == projectId && 
                cr.RequestTitle == title);
        }

        public async Task<IEnumerable<ChangeRequest>> SearchByTitleAsync(string searchTerm)
        {
            return await _context.ChangeRequests
                .Include(cr => cr.Project)
                .Include(cr => cr.RequestedByUser)
                .Include(cr => cr.ApprovedByUser)
                .Include(cr => cr.Tenant)
                .Include(cr => cr.Milestone)
                .Where(cr => cr.RequestTitle.Contains(searchTerm) || 
                           cr.RequestDescription.Contains(searchTerm) ||
                           cr.ReasonForChange.Contains(searchTerm))
                .ToListAsync();
        }
    }
} 
using Microsoft.EntityFrameworkCore;
using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Repositories
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly ApplicationDbContext _context;

        public ResourceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Resource>> GetAllAsync()
        {
            return await _context.Resources
                .Include(r => r.User)
                .Include(r => r.Tenant)
                .ToListAsync();
        }

        public async Task<Resource?> GetByIdAsync(Guid id)
        {
            return await _context.Resources
                .Include(r => r.User)
                .Include(r => r.Tenant)
                .FirstOrDefaultAsync(r => r.ResourceId == id);
        }

        public async Task<Resource?> GetByEmailAsync(string email)
        {
            return await _context.Resources
                .Include(r => r.User)
                .Include(r => r.Tenant)
                .FirstOrDefaultAsync(r => r.Email == email);
        }

        public async Task<IEnumerable<Resource>> GetByDepartmentAsync(string department)
        {
            return await _context.Resources
                .Include(r => r.User)
                .Include(r => r.Tenant)
                .Where(r => r.Department == department)
                .ToListAsync();
        }

        public async Task<IEnumerable<Resource>> GetByRoleTitleAsync(string roleTitle)
        {
            return await _context.Resources
                .Include(r => r.User)
                .Include(r => r.Tenant)
                .Where(r => r.RoleTitle == roleTitle)
                .ToListAsync();
        }

        public async Task<IEnumerable<Resource>> GetByEmploymentStatusAsync(string status)
        {
            return await _context.Resources
                .Include(r => r.User)
                .Include(r => r.Tenant)
                .Where(r => r.EmploymentStatus == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Resource>> GetByLocationAsync(string location)
        {
            return await _context.Resources
                .Include(r => r.User)
                .Include(r => r.Tenant)
                .Where(r => r.Location == location)
                .ToListAsync();
        }

        public async Task<IEnumerable<Resource>> GetBySkillsAsync(string[] skills)
        {
            return await _context.Resources
                .Include(r => r.User)
                .Include(r => r.Tenant)
                .Where(r => r.Skills != null && skills.Any(skill => r.Skills.Contains(skill)))
                .ToListAsync();
        }

        public async Task<IEnumerable<Resource>> GetActiveResourcesAsync()
        {
            return await _context.Resources
                .Include(r => r.User)
                .Include(r => r.Tenant)
                .Where(r => r.EmploymentStatus == "Active")
                .ToListAsync();
        }

        public async Task<IEnumerable<Resource>> GetAvailableResourcesAsync()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            return await _context.Resources
                .Include(r => r.User)
                .Include(r => r.Tenant)
                .Where(r => r.EmploymentStatus == "Active" &&
                           (!r.EndDateEmployment.HasValue || r.EndDateEmployment > today))
                .ToListAsync();
        }

        public async Task<IEnumerable<Resource>> GetByTenantIdAsync(Guid tenantId)
        {
            return await _context.Resources
                .Include(r => r.User)
                .Include(r => r.Tenant)
                .Where(r => r.TenantId == tenantId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Resource>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Resources
                .Include(r => r.User)
                .Include(r => r.Tenant)
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Resource>> GetByTenantAndStatusAsync(Guid tenantId, string status)
        {
            return await _context.Resources
                .Include(r => r.User)
                .Include(r => r.Tenant)
                .Where(r => r.TenantId == tenantId && r.EmploymentStatus == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Resource>> GetByTenantAndDepartmentAsync(Guid tenantId, string department)
        {
            return await _context.Resources
                .Include(r => r.User)
                .Include(r => r.Tenant)
                .Where(r => r.TenantId == tenantId && r.Department == department)
                .ToListAsync();
        }

        public async Task<IEnumerable<Resource>> GetByTenantAndRoleAsync(Guid tenantId, string roleTitle)
        {
            return await _context.Resources
                .Include(r => r.User)
                .Include(r => r.Tenant)
                .Where(r => r.TenantId == tenantId && r.RoleTitle == roleTitle)
                .ToListAsync();
        }

        public async Task<Resource?> GetByUserAndTenantAsync(Guid userId, Guid tenantId)
        {
            return await _context.Resources
                .Include(r => r.User)
                .Include(r => r.Tenant)
                .FirstOrDefaultAsync(r => r.UserId == userId && r.TenantId == tenantId);
        }

        public async Task<Resource> AddAsync(Resource resource)
        {
            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();
            return resource;
        }

        public async Task<Resource> UpdateAsync(Resource resource)
        {
            resource.UpdatedAt = DateTime.UtcNow;
            _context.Resources.Update(resource);
            await _context.SaveChangesAsync();
            return resource;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var resource = await _context.Resources.FindAsync(id);
            if (resource == null)
                return false;

            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Resources.AnyAsync(r => r.ResourceId == id);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Resources.AnyAsync(r => r.Email == email);
        }

        public async Task<bool> ExistsByUserAndTenantAsync(Guid userId, Guid tenantId, Guid? excludeResourceId = null)
        {
            if (excludeResourceId.HasValue)
            {
                return await _context.Resources.AnyAsync(r => 
                    r.UserId == userId && 
                    r.TenantId == tenantId && 
                    r.ResourceId != excludeResourceId.Value);
            }
            return await _context.Resources.AnyAsync(r => 
                r.UserId == userId && 
                r.TenantId == tenantId);
        }

        public async Task<IEnumerable<Resource>> SearchByNameAsync(string searchTerm)
        {
            return await _context.Resources
                .Include(r => r.User)
                .Include(r => r.Tenant)
                .Where(r => r.FirstName.Contains(searchTerm) || 
                           r.LastName.Contains(searchTerm) ||
                           r.FullName.Contains(searchTerm))
                .ToListAsync();
        }
    }
} 
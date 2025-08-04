using Microsoft.EntityFrameworkCore;
using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Repositories
{
    public class OrganizationUserRepository : IOrganizationUserRepository
    {
        private readonly ApplicationDbContext _context;

        public OrganizationUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrganizationUser>> GetAllAsync()
        {
            return await _context.OrganizationUsers.ToListAsync();
        }

        public async Task<OrganizationUser?> GetByOrganizationAndUserAsync(Guid organizationId, Guid userId)
        {
            return await _context.OrganizationUsers
                .FirstOrDefaultAsync(ou => ou.OrganizationId == organizationId && ou.UserId == userId);
        }

        public async Task<IEnumerable<OrganizationUser>> GetByOrganizationIdAsync(Guid organizationId)
        {
            return await _context.OrganizationUsers
                .Where(ou => ou.OrganizationId == organizationId)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrganizationUser>> GetByUserIdAsync(Guid userId)
        {
            return await _context.OrganizationUsers
                .Where(ou => ou.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrganizationUser>> GetActiveByOrganizationIdAsync(Guid organizationId)
        {
            return await _context.OrganizationUsers
                .Where(ou => ou.OrganizationId == organizationId && ou.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrganizationUser>> GetActiveByUserIdAsync(Guid userId)
        {
            return await _context.OrganizationUsers
                .Where(ou => ou.UserId == userId && ou.IsActive)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrganizationUser>> GetByRoleAsync(string role)
        {
            return await _context.OrganizationUsers
                .Where(ou => ou.Role == role)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrganizationUser>> GetByOrganizationAndRoleAsync(Guid organizationId, string role)
        {
            return await _context.OrganizationUsers
                .Where(ou => ou.OrganizationId == organizationId && ou.Role == role)
                .ToListAsync();
        }

        public async Task<OrganizationUser> AddAsync(OrganizationUser organizationUser)
        {
            _context.OrganizationUsers.Add(organizationUser);
            await _context.SaveChangesAsync();
            return organizationUser;
        }

        public async Task<OrganizationUser> UpdateAsync(OrganizationUser organizationUser)
        {
            organizationUser.UpdatedAt = DateTime.UtcNow;
            _context.OrganizationUsers.Update(organizationUser);
            await _context.SaveChangesAsync();
            return organizationUser;
        }

        public async Task<bool> DeleteAsync(Guid organizationId, Guid userId)
        {
            var organizationUser = await _context.OrganizationUsers
                .FirstOrDefaultAsync(ou => ou.OrganizationId == organizationId && ou.UserId == userId);
            
            if (organizationUser == null)
                return false;

            _context.OrganizationUsers.Remove(organizationUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid organizationId, Guid userId)
        {
            return await _context.OrganizationUsers
                .AnyAsync(ou => ou.OrganizationId == organizationId && ou.UserId == userId);
        }

        public async Task<bool> IsUserActiveInOrganizationAsync(Guid organizationId, Guid userId)
        {
            return await _context.OrganizationUsers
                .AnyAsync(ou => ou.OrganizationId == organizationId && 
                               ou.UserId == userId && 
                               ou.IsActive);
        }
    }
} 
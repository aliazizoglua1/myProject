using Microsoft.EntityFrameworkCore;
using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly ApplicationDbContext _context;

        public OrganizationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Organization>> GetAllAsync()
        {
            return await _context.Organizations.ToListAsync();
        }

        public async Task<Organization?> GetByIdAsync(Guid id)
        {
            return await _context.Organizations.FindAsync(id);
        }

        public async Task<Organization?> GetByNameAsync(string name)
        {
            return await _context.Organizations
                .FirstOrDefaultAsync(o => o.OrganizationName == name);
        }

        public async Task<IEnumerable<Organization>> GetByStatusAsync(string status)
        {
            return await _context.Organizations
                .Where(o => o.Status == status)
                .ToListAsync();
        }

        public async Task<Organization> AddAsync(Organization organization)
        {
            _context.Organizations.Add(organization);
            await _context.SaveChangesAsync();
            return organization;
        }

        public async Task<Organization> UpdateAsync(Organization organization)
        {
            organization.UpdatedAt = DateTime.UtcNow;
            _context.Organizations.Update(organization);
            await _context.SaveChangesAsync();
            return organization;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var organization = await _context.Organizations.FindAsync(id);
            if (organization == null)
                return false;

            _context.Organizations.Remove(organization);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Organizations.AnyAsync(o => o.OrganizationId == id);
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Organizations.AnyAsync(o => o.OrganizationName == name);
        }
    }
} 
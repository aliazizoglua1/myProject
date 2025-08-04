using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Repositories
{
    public interface IOrganizationUserRepository
    {
        Task<IEnumerable<OrganizationUser>> GetAllAsync();
        Task<OrganizationUser?> GetByOrganizationAndUserAsync(Guid organizationId, Guid userId);
        Task<IEnumerable<OrganizationUser>> GetByOrganizationIdAsync(Guid organizationId);
        Task<IEnumerable<OrganizationUser>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<OrganizationUser>> GetActiveByOrganizationIdAsync(Guid organizationId);
        Task<IEnumerable<OrganizationUser>> GetActiveByUserIdAsync(Guid userId);
        Task<IEnumerable<OrganizationUser>> GetByRoleAsync(string role);
        Task<IEnumerable<OrganizationUser>> GetByOrganizationAndRoleAsync(Guid organizationId, string role);
        Task<OrganizationUser> AddAsync(OrganizationUser organizationUser);
        Task<OrganizationUser> UpdateAsync(OrganizationUser organizationUser);
        Task<bool> DeleteAsync(Guid organizationId, Guid userId);
        Task<bool> ExistsAsync(Guid organizationId, Guid userId);
        Task<bool> IsUserActiveInOrganizationAsync(Guid organizationId, Guid userId);
    }
} 
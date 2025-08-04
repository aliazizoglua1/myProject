using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Repositories
{
    public interface IResourceRepository
    {
        Task<IEnumerable<Resource>> GetAllAsync();
        Task<Resource?> GetByIdAsync(Guid id);
        Task<Resource?> GetByEmailAsync(string email);
        Task<IEnumerable<Resource>> GetByDepartmentAsync(string department);
        Task<IEnumerable<Resource>> GetByRoleTitleAsync(string roleTitle);
        Task<IEnumerable<Resource>> GetByEmploymentStatusAsync(string status);
        Task<IEnumerable<Resource>> GetByLocationAsync(string location);
        Task<IEnumerable<Resource>> GetBySkillsAsync(string[] skills);
        Task<IEnumerable<Resource>> GetActiveResourcesAsync();
        Task<IEnumerable<Resource>> GetAvailableResourcesAsync();
        Task<Resource> AddAsync(Resource resource);
        Task<Resource> UpdateAsync(Resource resource);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> EmailExistsAsync(string email);
        Task<bool> ExistsByUserAndTenantAsync(Guid userId, Guid tenantId, Guid? excludeResourceId = null);
        Task<IEnumerable<Resource>> GetByTenantIdAsync(Guid tenantId);
        Task<IEnumerable<Resource>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<Resource>> GetByTenantAndStatusAsync(Guid tenantId, string status);
        Task<IEnumerable<Resource>> GetByTenantAndDepartmentAsync(Guid tenantId, string department);
        Task<IEnumerable<Resource>> GetByTenantAndRoleAsync(Guid tenantId, string roleTitle);
        Task<Resource?> GetByUserAndTenantAsync(Guid userId, Guid tenantId);
        Task<IEnumerable<Resource>> SearchByNameAsync(string searchTerm);
    }
} 
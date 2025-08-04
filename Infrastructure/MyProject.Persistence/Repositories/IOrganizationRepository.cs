using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Repositories
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<Organization>> GetAllAsync();
        Task<Organization?> GetByIdAsync(Guid id);
        Task<Organization?> GetByNameAsync(string name);
        Task<IEnumerable<Organization>> GetByStatusAsync(string status);
        Task<Organization> AddAsync(Organization organization);
        Task<Organization> UpdateAsync(Organization organization);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> ExistsByNameAsync(string name);
    }
} 
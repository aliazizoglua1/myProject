using MyProject.Persistence.Entities;

namespace MyProject.Persistence.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(Guid id);
        Task<IEnumerable<Project>> GetByStatusAsync(string status);
        Task<IEnumerable<Project>> GetByManagerIdAsync(Guid managerId);
        Task<Project> AddAsync(Project project);
        Task<Project> UpdateAsync(Project project);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
} 
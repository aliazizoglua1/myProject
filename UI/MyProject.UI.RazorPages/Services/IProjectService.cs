using MyProject.UI.RazorPages.Models;

namespace MyProject.UI.RazorPages.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
        Task<ProjectDto?> GetProjectAsync(Guid id);
        Task<ProjectDto> CreateProjectAsync(CreateProjectDto project);
        Task<ProjectDto> UpdateProjectAsync(Guid id, UpdateProjectDto project);
        Task<bool> DeleteProjectAsync(Guid id);
        Task<IEnumerable<ProjectDto>> GetProjectsByStatusAsync(string status);
        Task<IEnumerable<ProjectDto>> GetProjectsByManagerAsync(Guid managerId);
    }
}
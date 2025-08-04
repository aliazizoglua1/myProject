using System.Net.Http.Json;
using MyProject.UI.ProjectManagementConsole.Models;

namespace MyProject.UI.ProjectManagementConsole.Services
{
    public interface IProjectService
    {
        Task<List<ProjectDto>> GetProjectsAsync();
        Task<ProjectDto?> GetProjectAsync(Guid id);
        Task<ProjectDto> CreateProjectAsync(CreateProjectDto project);
        Task<ProjectDto> UpdateProjectAsync(Guid id, UpdateProjectDto project);
        Task<bool> DeleteProjectAsync(Guid id);
        Task<List<ProjectDto>> GetProjectsByOrganizationAsync(Guid organizationId);
        Task<List<ProjectDto>> GetProjectsByStatusAsync(string status);
        Task<List<ProjectDto>> GetProjectsByManagerAsync(Guid managerId);
    }

    public class ProjectService : IProjectService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7001"; // Update this to match your API URL

        public ProjectService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProjectDto>> GetProjectsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ProjectDto>>($"{_baseUrl}/api/projects");
                return response ?? new List<ProjectDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching projects: {ex.Message}");
                return new List<ProjectDto>();
            }
        }

        public async Task<ProjectDto?> GetProjectAsync(Guid id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ProjectDto>($"{_baseUrl}/api/projects/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching project: {ex.Message}");
                return null;
            }
        }

        public async Task<ProjectDto> CreateProjectAsync(CreateProjectDto project)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/projects", project);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<ProjectDto>() ?? new ProjectDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating project: {ex.Message}");
                throw;
            }
        }

        public async Task<ProjectDto> UpdateProjectAsync(Guid id, UpdateProjectDto project)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/projects/{id}", project);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<ProjectDto>() ?? new ProjectDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating project: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteProjectAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/projects/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting project: {ex.Message}");
                return false;
            }
        }

        public async Task<List<ProjectDto>> GetProjectsByOrganizationAsync(Guid organizationId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ProjectDto>>($"{_baseUrl}/api/projects/organization/{organizationId}");
                return response ?? new List<ProjectDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching projects by organization: {ex.Message}");
                return new List<ProjectDto>();
            }
        }

        public async Task<List<ProjectDto>> GetProjectsByStatusAsync(string status)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ProjectDto>>($"{_baseUrl}/api/projects/status/{status}");
                return response ?? new List<ProjectDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching projects by status: {ex.Message}");
                return new List<ProjectDto>();
            }
        }

        public async Task<List<ProjectDto>> GetProjectsByManagerAsync(Guid managerId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ProjectDto>>($"{_baseUrl}/api/projects/manager/{managerId}");
                return response ?? new List<ProjectDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching projects by manager: {ex.Message}");
                return new List<ProjectDto>();
            }
        }
    }
} 
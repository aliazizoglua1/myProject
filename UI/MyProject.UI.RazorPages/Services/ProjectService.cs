using MyProject.UI.RazorPages.Models;
using System.Text.Json;
using System.Text;

namespace MyProject.UI.RazorPages.Services
{
    public class ProjectService : IProjectService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public ProjectService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
        {
            var response = await _httpClient.GetAsync("api/projects");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var projects = JsonSerializer.Deserialize<IEnumerable<ProjectDto>>(json, _jsonOptions);
            return projects ?? new List<ProjectDto>();
        }

        public async Task<ProjectDto?> GetProjectAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/projects/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ProjectDto>(json, _jsonOptions);
        }

        public async Task<ProjectDto> CreateProjectAsync(CreateProjectDto project)
        {
            var json = JsonSerializer.Serialize(project, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync("api/projects", content);
            response.EnsureSuccessStatusCode();
            
            var responseJson = await response.Content.ReadAsStringAsync();
            var createdProject = JsonSerializer.Deserialize<ProjectDto>(responseJson, _jsonOptions);
            return createdProject ?? throw new InvalidOperationException("Failed to create project");
        }

        public async Task<ProjectDto> UpdateProjectAsync(Guid id, UpdateProjectDto project)
        {
            var json = JsonSerializer.Serialize(project, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PutAsync($"api/projects/{id}", content);
            response.EnsureSuccessStatusCode();
            
            var responseJson = await response.Content.ReadAsStringAsync();
            var updatedProject = JsonSerializer.Deserialize<ProjectDto>(responseJson, _jsonOptions);
            return updatedProject ?? throw new InvalidOperationException("Failed to update project");
        }

        public async Task<bool> DeleteProjectAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/projects/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<ProjectDto>> GetProjectsByStatusAsync(string status)
        {
            var response = await _httpClient.GetAsync($"api/projects/status/{status}");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var projects = JsonSerializer.Deserialize<IEnumerable<ProjectDto>>(json, _jsonOptions);
            return projects ?? new List<ProjectDto>();
        }

        public async Task<IEnumerable<ProjectDto>> GetProjectsByManagerAsync(Guid managerId)
        {
            var response = await _httpClient.GetAsync($"api/projects/manager/{managerId}");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var projects = JsonSerializer.Deserialize<IEnumerable<ProjectDto>>(json, _jsonOptions);
            return projects ?? new List<ProjectDto>();
        }
    }
}
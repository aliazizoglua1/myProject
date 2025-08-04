using System.Net.Http.Json;
using MyProject.UI.ProjectManagementConsole.Models;

namespace MyProject.UI.ProjectManagementConsole.Services
{
    public interface IIssueService
    {
        Task<List<IssueDto>> GetIssuesAsync();
        Task<IssueDto?> GetIssueAsync(Guid id);
        Task<IssueDto> CreateIssueAsync(CreateIssueDto issue);
        Task<IssueDto> UpdateIssueAsync(Guid id, UpdateIssueDto issue);
        Task<bool> DeleteIssueAsync(Guid id);
        Task<List<IssueDto>> GetByTenantAsync(Guid tenantId);
        Task<List<IssueDto>> GetByTenantAndStatusAsync(Guid tenantId, string status);
        Task<List<IssueDto>> GetByTenantAndPriorityAsync(Guid tenantId, string priority);
        Task<List<IssueDto>> GetByTenantAndSeverityAsync(Guid tenantId, string severity);
        Task<List<IssueDto>> GetByProjectAndStatusAsync(Guid projectId, string status);
        Task<List<IssueDto>> GetByMilestoneIdAsync(Guid milestoneId);
        Task<List<IssueDto>> GetByTenantAndUserAsync(Guid tenantId, Guid userId);
        Task<bool> ExistsByProjectAndNameAsync(Guid projectId, string name);
    }

    public class IssueService : IIssueService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7001"; // Update this to match your API URL

        public IssueService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<IssueDto>> GetIssuesAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<IssueDto>>($"{_baseUrl}/api/issues");
                return response ?? new List<IssueDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching issues: {ex.Message}");
                return new List<IssueDto>();
            }
        }

        public async Task<IssueDto?> GetIssueAsync(Guid id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IssueDto>($"{_baseUrl}/api/issues/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching issue: {ex.Message}");
                return null;
            }
        }

        public async Task<IssueDto> CreateIssueAsync(CreateIssueDto issue)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/issues", issue);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<IssueDto>() ?? new IssueDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating issue: {ex.Message}");
                throw;
            }
        }

        public async Task<IssueDto> UpdateIssueAsync(Guid id, UpdateIssueDto issue)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/issues/{id}", issue);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<IssueDto>() ?? new IssueDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating issue: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteIssueAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/issues/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting issue: {ex.Message}");
                return false;
            }
        }

        public async Task<List<IssueDto>> GetByTenantAsync(Guid tenantId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<IssueDto>>($"{_baseUrl}/api/issues/tenant/{tenantId}");
                return response ?? new List<IssueDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching issues by tenant: {ex.Message}");
                return new List<IssueDto>();
            }
        }

        public async Task<List<IssueDto>> GetByTenantAndStatusAsync(Guid tenantId, string status)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<IssueDto>>($"{_baseUrl}/api/issues/tenant/{tenantId}/status/{status}");
                return response ?? new List<IssueDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching issues by tenant and status: {ex.Message}");
                return new List<IssueDto>();
            }
        }

        public async Task<List<IssueDto>> GetByTenantAndPriorityAsync(Guid tenantId, string priority)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<IssueDto>>($"{_baseUrl}/api/issues/tenant/{tenantId}/priority/{priority}");
                return response ?? new List<IssueDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching issues by tenant and priority: {ex.Message}");
                return new List<IssueDto>();
            }
        }

        public async Task<List<IssueDto>> GetByTenantAndSeverityAsync(Guid tenantId, string severity)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<IssueDto>>($"{_baseUrl}/api/issues/tenant/{tenantId}/severity/{severity}");
                return response ?? new List<IssueDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching issues by tenant and severity: {ex.Message}");
                return new List<IssueDto>();
            }
        }

        public async Task<List<IssueDto>> GetByProjectAndStatusAsync(Guid projectId, string status)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<IssueDto>>($"{_baseUrl}/api/issues/project/{projectId}/status/{status}");
                return response ?? new List<IssueDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching issues by project and status: {ex.Message}");
                return new List<IssueDto>();
            }
        }

        public async Task<List<IssueDto>> GetByMilestoneIdAsync(Guid milestoneId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<IssueDto>>($"{_baseUrl}/api/issues/milestone/{milestoneId}");
                return response ?? new List<IssueDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching issues by milestone: {ex.Message}");
                return new List<IssueDto>();
            }
        }

        public async Task<List<IssueDto>> GetByTenantAndUserAsync(Guid tenantId, Guid userId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<IssueDto>>($"{_baseUrl}/api/issues/tenant/{tenantId}/user/{userId}");
                return response ?? new List<IssueDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching issues by tenant and user: {ex.Message}");
                return new List<IssueDto>();
            }
        }

        public async Task<bool> ExistsByProjectAndNameAsync(Guid projectId, string name)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/issues/exists/project/{projectId}/name/{name}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking issue existence: {ex.Message}");
                return false;
            }
        }
    }
} 
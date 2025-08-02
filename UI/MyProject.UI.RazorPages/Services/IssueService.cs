using MyProject.UI.RazorPages.Models;
using System.Text.Json;
using System.Text;

namespace MyProject.UI.RazorPages.Services
{
    public class IssueService : IIssueService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public IssueService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<IEnumerable<IssueDto>> GetAllIssuesAsync()
        {
            var response = await _httpClient.GetAsync("api/issues");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var issues = JsonSerializer.Deserialize<IEnumerable<IssueDto>>(json, _jsonOptions);
            return issues ?? new List<IssueDto>();
        }

        public async Task<IssueDto?> GetIssueAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/issues/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IssueDto>(json, _jsonOptions);
        }

        public async Task<IssueDto> CreateIssueAsync(CreateIssueDto issue)
        {
            var json = JsonSerializer.Serialize(issue, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync("api/issues", content);
            response.EnsureSuccessStatusCode();
            
            var responseJson = await response.Content.ReadAsStringAsync();
            var createdIssue = JsonSerializer.Deserialize<IssueDto>(responseJson, _jsonOptions);
            return createdIssue ?? throw new InvalidOperationException("Failed to create issue");
        }

        public async Task<IssueDto> UpdateIssueAsync(Guid id, UpdateIssueDto issue)
        {
            var json = JsonSerializer.Serialize(issue, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PutAsync($"api/issues/{id}", content);
            response.EnsureSuccessStatusCode();
            
            var responseJson = await response.Content.ReadAsStringAsync();
            var updatedIssue = JsonSerializer.Deserialize<IssueDto>(responseJson, _jsonOptions);
            return updatedIssue ?? throw new InvalidOperationException("Failed to update issue");
        }

        public async Task<bool> DeleteIssueAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/issues/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<IssueDto>> GetIssuesByProjectAsync(Guid projectId)
        {
            var response = await _httpClient.GetAsync($"api/issues/project/{projectId}");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var issues = JsonSerializer.Deserialize<IEnumerable<IssueDto>>(json, _jsonOptions);
            return issues ?? new List<IssueDto>();
        }

        public async Task<IEnumerable<IssueDto>> GetIssuesByTaskAsync(Guid taskId)
        {
            var response = await _httpClient.GetAsync($"api/issues/task/{taskId}");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var issues = JsonSerializer.Deserialize<IEnumerable<IssueDto>>(json, _jsonOptions);
            return issues ?? new List<IssueDto>();
        }

        public async Task<IEnumerable<IssueDto>> GetIssuesByStatusAsync(string status)
        {
            var response = await _httpClient.GetAsync($"api/issues/status/{status}");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var issues = JsonSerializer.Deserialize<IEnumerable<IssueDto>>(json, _jsonOptions);
            return issues ?? new List<IssueDto>();
        }

        public async Task<IEnumerable<IssueDto>> GetIssuesByPriorityAsync(string priority)
        {
            var response = await _httpClient.GetAsync($"api/issues/priority/{priority}");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var issues = JsonSerializer.Deserialize<IEnumerable<IssueDto>>(json, _jsonOptions);
            return issues ?? new List<IssueDto>();
        }

        public async Task<IEnumerable<IssueDto>> GetIssuesBySeverityAsync(string severity)
        {
            var response = await _httpClient.GetAsync($"api/issues/severity/{severity}");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var issues = JsonSerializer.Deserialize<IEnumerable<IssueDto>>(json, _jsonOptions);
            return issues ?? new List<IssueDto>();
        }

        public async Task<IEnumerable<IssueDto>> GetIssuesByAssignedUserAsync(Guid userId)
        {
            var response = await _httpClient.GetAsync($"api/issues/assigned/{userId}");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var issues = JsonSerializer.Deserialize<IEnumerable<IssueDto>>(json, _jsonOptions);
            return issues ?? new List<IssueDto>();
        }

        public async Task<IEnumerable<IssueDto>> GetIssuesByTypeAsync(string issueType)
        {
            var response = await _httpClient.GetAsync($"api/issues/type/{issueType}");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var issues = JsonSerializer.Deserialize<IEnumerable<IssueDto>>(json, _jsonOptions);
            return issues ?? new List<IssueDto>();
        }

        public async Task<IEnumerable<IssueDto>> GetOpenIssuesAsync()
        {
            var response = await _httpClient.GetAsync("api/issues/open");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var issues = JsonSerializer.Deserialize<IEnumerable<IssueDto>>(json, _jsonOptions);
            return issues ?? new List<IssueDto>();
        }

        public async Task<IEnumerable<IssueDto>> GetCriticalIssuesAsync()
        {
            var response = await _httpClient.GetAsync("api/issues/critical");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var issues = JsonSerializer.Deserialize<IEnumerable<IssueDto>>(json, _jsonOptions);
            return issues ?? new List<IssueDto>();
        }

        public async Task<IEnumerable<IssueDto>> GetOverdueIssuesAsync()
        {
            var response = await _httpClient.GetAsync("api/issues/overdue");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var issues = JsonSerializer.Deserialize<IEnumerable<IssueDto>>(json, _jsonOptions);
            return issues ?? new List<IssueDto>();
        }

        public async Task<IEnumerable<IssueDto>> SearchIssuesAsync(string searchTerm)
        {
            var response = await _httpClient.GetAsync($"api/issues/search?searchTerm={Uri.EscapeDataString(searchTerm)}");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var issues = JsonSerializer.Deserialize<IEnumerable<IssueDto>>(json, _jsonOptions);
            return issues ?? new List<IssueDto>();
        }
    }
}
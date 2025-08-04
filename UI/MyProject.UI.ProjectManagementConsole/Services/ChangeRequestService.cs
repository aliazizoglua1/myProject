using System.Net.Http.Json;
using MyProject.UI.ProjectManagementConsole.Models;

namespace MyProject.UI.ProjectManagementConsole.Services
{
    public interface IChangeRequestService
    {
        Task<List<ChangeRequestDto>> GetChangeRequestsAsync();
        Task<ChangeRequestDto?> GetChangeRequestAsync(Guid id);
        Task<ChangeRequestDto> CreateChangeRequestAsync(CreateChangeRequestDto changeRequest);
        Task<ChangeRequestDto> UpdateChangeRequestAsync(Guid id, UpdateChangeRequestDto changeRequest);
        Task<bool> DeleteChangeRequestAsync(Guid id);
        Task<List<ChangeRequestDto>> GetByTenantAsync(Guid tenantId);
        Task<List<ChangeRequestDto>> GetByTenantAndStatusAsync(Guid tenantId, string status);
        Task<List<ChangeRequestDto>> GetByTenantAndTypeAsync(Guid tenantId, string type);
        Task<List<ChangeRequestDto>> GetByProjectAndStatusAsync(Guid projectId, string status);
        Task<List<ChangeRequestDto>> GetByMilestoneIdAsync(Guid milestoneId);
        Task<List<ChangeRequestDto>> GetByTenantAndUserAsync(Guid tenantId, Guid userId);
        Task<List<ChangeRequestDto>> GetPendingChangeRequestsAsync();
        Task<List<ChangeRequestDto>> GetApprovedChangeRequestsAsync();
        Task<List<ChangeRequestDto>> GetRejectedChangeRequestsAsync();
        Task<bool> ExistsByProjectAndTitleAsync(Guid projectId, string title);
    }

    public class ChangeRequestService : IChangeRequestService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7001"; // Update this to match your API URL

        public ChangeRequestService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ChangeRequestDto>> GetChangeRequestsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ChangeRequestDto>>($"{_baseUrl}/api/changerequests");
                return response ?? new List<ChangeRequestDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching change requests: {ex.Message}");
                return new List<ChangeRequestDto>();
            }
        }

        public async Task<ChangeRequestDto?> GetChangeRequestAsync(Guid id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ChangeRequestDto>($"{_baseUrl}/api/changerequests/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching change request: {ex.Message}");
                return null;
            }
        }

        public async Task<ChangeRequestDto> CreateChangeRequestAsync(CreateChangeRequestDto changeRequest)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/changerequests", changeRequest);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<ChangeRequestDto>() ?? new ChangeRequestDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating change request: {ex.Message}");
                throw;
            }
        }

        public async Task<ChangeRequestDto> UpdateChangeRequestAsync(Guid id, UpdateChangeRequestDto changeRequest)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/changerequests/{id}", changeRequest);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<ChangeRequestDto>() ?? new ChangeRequestDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating change request: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteChangeRequestAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/changerequests/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting change request: {ex.Message}");
                return false;
            }
        }

        public async Task<List<ChangeRequestDto>> GetByTenantAsync(Guid tenantId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ChangeRequestDto>>($"{_baseUrl}/api/changerequests/tenant/{tenantId}");
                return response ?? new List<ChangeRequestDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching change requests by tenant: {ex.Message}");
                return new List<ChangeRequestDto>();
            }
        }

        public async Task<List<ChangeRequestDto>> GetByTenantAndStatusAsync(Guid tenantId, string status)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ChangeRequestDto>>($"{_baseUrl}/api/changerequests/tenant/{tenantId}/status/{status}");
                return response ?? new List<ChangeRequestDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching change requests by tenant and status: {ex.Message}");
                return new List<ChangeRequestDto>();
            }
        }

        public async Task<List<ChangeRequestDto>> GetByTenantAndTypeAsync(Guid tenantId, string type)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ChangeRequestDto>>($"{_baseUrl}/api/changerequests/tenant/{tenantId}/type/{type}");
                return response ?? new List<ChangeRequestDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching change requests by tenant and type: {ex.Message}");
                return new List<ChangeRequestDto>();
            }
        }

        public async Task<List<ChangeRequestDto>> GetByProjectAndStatusAsync(Guid projectId, string status)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ChangeRequestDto>>($"{_baseUrl}/api/changerequests/project/{projectId}/status/{status}");
                return response ?? new List<ChangeRequestDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching change requests by project and status: {ex.Message}");
                return new List<ChangeRequestDto>();
            }
        }

        public async Task<List<ChangeRequestDto>> GetByMilestoneIdAsync(Guid milestoneId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ChangeRequestDto>>($"{_baseUrl}/api/changerequests/milestone/{milestoneId}");
                return response ?? new List<ChangeRequestDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching change requests by milestone: {ex.Message}");
                return new List<ChangeRequestDto>();
            }
        }

        public async Task<List<ChangeRequestDto>> GetByTenantAndUserAsync(Guid tenantId, Guid userId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ChangeRequestDto>>($"{_baseUrl}/api/changerequests/tenant/{tenantId}/user/{userId}");
                return response ?? new List<ChangeRequestDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching change requests by tenant and user: {ex.Message}");
                return new List<ChangeRequestDto>();
            }
        }

        public async Task<List<ChangeRequestDto>> GetPendingChangeRequestsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ChangeRequestDto>>($"{_baseUrl}/api/changerequests/pending");
                return response ?? new List<ChangeRequestDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching pending change requests: {ex.Message}");
                return new List<ChangeRequestDto>();
            }
        }

        public async Task<List<ChangeRequestDto>> GetApprovedChangeRequestsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ChangeRequestDto>>($"{_baseUrl}/api/changerequests/approved");
                return response ?? new List<ChangeRequestDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching approved change requests: {ex.Message}");
                return new List<ChangeRequestDto>();
            }
        }

        public async Task<List<ChangeRequestDto>> GetRejectedChangeRequestsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ChangeRequestDto>>($"{_baseUrl}/api/changerequests/rejected");
                return response ?? new List<ChangeRequestDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching rejected change requests: {ex.Message}");
                return new List<ChangeRequestDto>();
            }
        }

        public async Task<bool> ExistsByProjectAndTitleAsync(Guid projectId, string title)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/changerequests/exists/project/{projectId}/title/{title}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking change request existence: {ex.Message}");
                return false;
            }
        }
    }
} 
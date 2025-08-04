using System.Net.Http.Json;
using MyProject.UI.ProjectManagementConsole.Models;

namespace MyProject.UI.ProjectManagementConsole.Services
{
    public interface IRiskService
    {
        Task<List<RiskDto>> GetRisksAsync();
        Task<RiskDto?> GetRiskAsync(Guid id);
        Task<RiskDto> CreateRiskAsync(CreateRiskDto risk);
        Task<RiskDto> UpdateRiskAsync(Guid id, UpdateRiskDto risk);
        Task<bool> DeleteRiskAsync(Guid id);
        Task<List<RiskDto>> GetByTenantAsync(Guid tenantId);
        Task<List<RiskDto>> GetByTenantAndStatusAsync(Guid tenantId, string status);
        Task<List<RiskDto>> GetByTenantAndCategoryAsync(Guid tenantId, string category);
        Task<List<RiskDto>> GetByProjectAndStatusAsync(Guid projectId, string status);
        Task<List<RiskDto>> GetByTenantAndOwnerAsync(Guid tenantId, Guid ownerId);
        Task<List<RiskDto>> GetOverdueRisksAsync();
        Task<List<RiskDto>> GetByExposureRangeAsync(int minExposure, int maxExposure);
    }

    public class RiskService : IRiskService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7001"; // Update this to match your API URL

        public RiskService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RiskDto>> GetRisksAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<RiskDto>>($"{_baseUrl}/api/risks");
                return response ?? new List<RiskDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching risks: {ex.Message}");
                return new List<RiskDto>();
            }
        }

        public async Task<RiskDto?> GetRiskAsync(Guid id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<RiskDto>($"{_baseUrl}/api/risks/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching risk: {ex.Message}");
                return null;
            }
        }

        public async Task<RiskDto> CreateRiskAsync(CreateRiskDto risk)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/risks", risk);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<RiskDto>() ?? new RiskDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating risk: {ex.Message}");
                throw;
            }
        }

        public async Task<RiskDto> UpdateRiskAsync(Guid id, UpdateRiskDto risk)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/risks/{id}", risk);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<RiskDto>() ?? new RiskDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating risk: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteRiskAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/risks/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting risk: {ex.Message}");
                return false;
            }
        }

        public async Task<List<RiskDto>> GetByTenantAsync(Guid tenantId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<RiskDto>>($"{_baseUrl}/api/risks/tenant/{tenantId}");
                return response ?? new List<RiskDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching risks by tenant: {ex.Message}");
                return new List<RiskDto>();
            }
        }

        public async Task<List<RiskDto>> GetByTenantAndStatusAsync(Guid tenantId, string status)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<RiskDto>>($"{_baseUrl}/api/risks/tenant/{tenantId}/status/{status}");
                return response ?? new List<RiskDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching risks by tenant and status: {ex.Message}");
                return new List<RiskDto>();
            }
        }

        public async Task<List<RiskDto>> GetByTenantAndCategoryAsync(Guid tenantId, string category)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<RiskDto>>($"{_baseUrl}/api/risks/tenant/{tenantId}/category/{category}");
                return response ?? new List<RiskDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching risks by tenant and category: {ex.Message}");
                return new List<RiskDto>();
            }
        }

        public async Task<List<RiskDto>> GetByProjectAndStatusAsync(Guid projectId, string status)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<RiskDto>>($"{_baseUrl}/api/risks/project/{projectId}/status/{status}");
                return response ?? new List<RiskDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching risks by project and status: {ex.Message}");
                return new List<RiskDto>();
            }
        }

        public async Task<List<RiskDto>> GetByTenantAndOwnerAsync(Guid tenantId, Guid ownerId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<RiskDto>>($"{_baseUrl}/api/risks/tenant/{tenantId}/owner/{ownerId}");
                return response ?? new List<RiskDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching risks by tenant and owner: {ex.Message}");
                return new List<RiskDto>();
            }
        }

        public async Task<List<RiskDto>> GetOverdueRisksAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<RiskDto>>($"{_baseUrl}/api/risks/overdue");
                return response ?? new List<RiskDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching overdue risks: {ex.Message}");
                return new List<RiskDto>();
            }
        }

        public async Task<List<RiskDto>> GetByExposureRangeAsync(int minExposure, int maxExposure)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<RiskDto>>($"{_baseUrl}/api/risks/exposure-range/{minExposure}/{maxExposure}");
                return response ?? new List<RiskDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching risks by exposure range: {ex.Message}");
                return new List<RiskDto>();
            }
        }
    }
} 
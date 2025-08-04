using System.Net.Http.Json;
using MyProject.UI.ProjectManagementConsole.Models;

namespace MyProject.UI.ProjectManagementConsole.Services
{
    public interface IResourceService
    {
        Task<List<ResourceDto>> GetResourcesAsync();
        Task<ResourceDto?> GetResourceAsync(Guid id);
        Task<ResourceDto> CreateResourceAsync(CreateResourceDto resource);
        Task<ResourceDto> UpdateResourceAsync(Guid id, UpdateResourceDto resource);
        Task<bool> DeleteResourceAsync(Guid id);
        Task<List<ResourceDto>> GetByTenantAsync(Guid tenantId);
        Task<List<ResourceDto>> GetByTenantAndStatusAsync(Guid tenantId, string status);
        Task<List<ResourceDto>> GetByTenantAndDepartmentAsync(Guid tenantId, string department);
        Task<List<ResourceDto>> GetByTenantAndRoleAsync(Guid tenantId, string role);
        Task<List<ResourceDto>> GetByUserIdAsync(Guid userId);
        Task<List<ResourceDto>> GetByUserAndTenantAsync(Guid userId, Guid tenantId);
        Task<bool> ExistsByUserAndTenantAsync(Guid userId, Guid tenantId);
    }

    public class ResourceService : IResourceService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7001"; // Update this to match your API URL

        public ResourceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ResourceDto>> GetResourcesAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ResourceDto>>($"{_baseUrl}/api/resources");
                return response ?? new List<ResourceDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching resources: {ex.Message}");
                return new List<ResourceDto>();
            }
        }

        public async Task<ResourceDto?> GetResourceAsync(Guid id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ResourceDto>($"{_baseUrl}/api/resources/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching resource: {ex.Message}");
                return null;
            }
        }

        public async Task<ResourceDto> CreateResourceAsync(CreateResourceDto resource)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/resources", resource);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<ResourceDto>() ?? new ResourceDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating resource: {ex.Message}");
                throw;
            }
        }

        public async Task<ResourceDto> UpdateResourceAsync(Guid id, UpdateResourceDto resource)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/resources/{id}", resource);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<ResourceDto>() ?? new ResourceDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating resource: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteResourceAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/resources/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting resource: {ex.Message}");
                return false;
            }
        }

        public async Task<List<ResourceDto>> GetByTenantAsync(Guid tenantId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ResourceDto>>($"{_baseUrl}/api/resources/tenant/{tenantId}");
                return response ?? new List<ResourceDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching resources by tenant: {ex.Message}");
                return new List<ResourceDto>();
            }
        }

        public async Task<List<ResourceDto>> GetByTenantAndStatusAsync(Guid tenantId, string status)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ResourceDto>>($"{_baseUrl}/api/resources/tenant/{tenantId}/status/{status}");
                return response ?? new List<ResourceDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching resources by tenant and status: {ex.Message}");
                return new List<ResourceDto>();
            }
        }

        public async Task<List<ResourceDto>> GetByTenantAndDepartmentAsync(Guid tenantId, string department)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ResourceDto>>($"{_baseUrl}/api/resources/tenant/{tenantId}/department/{department}");
                return response ?? new List<ResourceDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching resources by tenant and department: {ex.Message}");
                return new List<ResourceDto>();
            }
        }

        public async Task<List<ResourceDto>> GetByTenantAndRoleAsync(Guid tenantId, string role)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ResourceDto>>($"{_baseUrl}/api/resources/tenant/{tenantId}/role/{role}");
                return response ?? new List<ResourceDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching resources by tenant and role: {ex.Message}");
                return new List<ResourceDto>();
            }
        }

        public async Task<List<ResourceDto>> GetByUserIdAsync(Guid userId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ResourceDto>>($"{_baseUrl}/api/resources/user/{userId}");
                return response ?? new List<ResourceDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching resources by user: {ex.Message}");
                return new List<ResourceDto>();
            }
        }

        public async Task<List<ResourceDto>> GetByUserAndTenantAsync(Guid userId, Guid tenantId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ResourceDto>>($"{_baseUrl}/api/resources/user/{userId}/tenant/{tenantId}");
                return response ?? new List<ResourceDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching resources by user and tenant: {ex.Message}");
                return new List<ResourceDto>();
            }
        }

        public async Task<bool> ExistsByUserAndTenantAsync(Guid userId, Guid tenantId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/resources/exists/user/{userId}/tenant/{tenantId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking resource existence: {ex.Message}");
                return false;
            }
        }
    }
} 
using System.Net.Http.Json;
using MyProject.UI.ProjectManagementConsole.Models;

namespace MyProject.UI.ProjectManagementConsole.Services
{
    public interface IOrganizationUserService
    {
        Task<List<OrganizationUserDto>> GetOrganizationUsersAsync();
        Task<OrganizationUserDto?> GetOrganizationUserAsync(Guid organizationId, Guid userId);
        Task<OrganizationUserDto> CreateOrganizationUserAsync(CreateOrganizationUserDto organizationUser);
        Task<OrganizationUserDto> UpdateOrganizationUserAsync(Guid organizationId, Guid userId, UpdateOrganizationUserDto organizationUser);
        Task<bool> DeleteOrganizationUserAsync(Guid organizationId, Guid userId);
        Task<List<OrganizationUserDto>> GetByOrganizationAsync(Guid organizationId);
        Task<List<OrganizationUserDto>> GetByUserAsync(Guid userId);
        Task<List<OrganizationUserDto>> GetActiveOrganizationUsersAsync();
        Task<OrganizationUserWithDetailsDto?> GetOrganizationUserWithDetailsAsync(Guid organizationId, Guid userId);
    }

    public class OrganizationUserService : IOrganizationUserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7001"; // Update this to match your API URL

        public OrganizationUserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<OrganizationUserDto>> GetOrganizationUsersAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<OrganizationUserDto>>($"{_baseUrl}/api/organizationusers");
                return response ?? new List<OrganizationUserDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching organization users: {ex.Message}");
                return new List<OrganizationUserDto>();
            }
        }

        public async Task<OrganizationUserDto?> GetOrganizationUserAsync(Guid organizationId, Guid userId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<OrganizationUserDto>($"{_baseUrl}/api/organizationusers/{organizationId}/{userId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching organization user: {ex.Message}");
                return null;
            }
        }

        public async Task<OrganizationUserDto> CreateOrganizationUserAsync(CreateOrganizationUserDto organizationUser)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/organizationusers", organizationUser);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<OrganizationUserDto>() ?? new OrganizationUserDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating organization user: {ex.Message}");
                throw;
            }
        }

        public async Task<OrganizationUserDto> UpdateOrganizationUserAsync(Guid organizationId, Guid userId, UpdateOrganizationUserDto organizationUser)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/organizationusers/{organizationId}/{userId}", organizationUser);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<OrganizationUserDto>() ?? new OrganizationUserDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating organization user: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteOrganizationUserAsync(Guid organizationId, Guid userId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/organizationusers/{organizationId}/{userId}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting organization user: {ex.Message}");
                return false;
            }
        }

        public async Task<List<OrganizationUserDto>> GetByOrganizationAsync(Guid organizationId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<OrganizationUserDto>>($"{_baseUrl}/api/organizationusers/organization/{organizationId}");
                return response ?? new List<OrganizationUserDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching organization users by organization: {ex.Message}");
                return new List<OrganizationUserDto>();
            }
        }

        public async Task<List<OrganizationUserDto>> GetByUserAsync(Guid userId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<OrganizationUserDto>>($"{_baseUrl}/api/organizationusers/user/{userId}");
                return response ?? new List<OrganizationUserDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching organization users by user: {ex.Message}");
                return new List<OrganizationUserDto>();
            }
        }

        public async Task<List<OrganizationUserDto>> GetActiveOrganizationUsersAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<OrganizationUserDto>>($"{_baseUrl}/api/organizationusers/active");
                return response ?? new List<OrganizationUserDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching active organization users: {ex.Message}");
                return new List<OrganizationUserDto>();
            }
        }

        public async Task<OrganizationUserWithDetailsDto?> GetOrganizationUserWithDetailsAsync(Guid organizationId, Guid userId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<OrganizationUserWithDetailsDto>($"{_baseUrl}/api/organizationusers/{organizationId}/{userId}/details");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching organization user with details: {ex.Message}");
                return null;
            }
        }
    }
} 
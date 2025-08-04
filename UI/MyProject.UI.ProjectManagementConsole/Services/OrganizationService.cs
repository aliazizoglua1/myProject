using System.Net.Http.Json;
using MyProject.UI.ProjectManagementConsole.Models;

namespace MyProject.UI.ProjectManagementConsole.Services
{
    public interface IOrganizationService
    {
        Task<List<OrganizationDto>> GetOrganizationsAsync();
        Task<OrganizationDto?> GetOrganizationAsync(Guid id);
        Task<OrganizationDto> CreateOrganizationAsync(CreateOrganizationDto organization);
        Task<OrganizationDto> UpdateOrganizationAsync(Guid id, UpdateOrganizationDto organization);
        Task<bool> DeleteOrganizationAsync(Guid id);
    }

    public class OrganizationService : IOrganizationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7001"; // Update this to match your API URL

        public OrganizationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<OrganizationDto>> GetOrganizationsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<OrganizationDto>>($"{_baseUrl}/api/organizations");
                return response ?? new List<OrganizationDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching organizations: {ex.Message}");
                return new List<OrganizationDto>();
            }
        }

        public async Task<OrganizationDto?> GetOrganizationAsync(Guid id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<OrganizationDto>($"{_baseUrl}/api/organizations/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching organization: {ex.Message}");
                return null;
            }
        }

        public async Task<OrganizationDto> CreateOrganizationAsync(CreateOrganizationDto organization)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/organizations", organization);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<OrganizationDto>() ?? new OrganizationDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating organization: {ex.Message}");
                throw;
            }
        }

        public async Task<OrganizationDto> UpdateOrganizationAsync(Guid id, UpdateOrganizationDto organization)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/organizations/{id}", organization);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<OrganizationDto>() ?? new OrganizationDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating organization: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteOrganizationAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/organizations/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting organization: {ex.Message}");
                return false;
            }
        }
    }
} 
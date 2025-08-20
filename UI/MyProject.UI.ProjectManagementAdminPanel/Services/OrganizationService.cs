using System.Net.Http.Json;
using MyProject.UI.ProjectManagementAdminPanel.Models;

namespace MyProject.UI.ProjectManagementAdminPanel.Services
{
    public interface IOrganizationService
    {
        Task<List<OrganizationDto>> GetOrganizationsAsync();
        Task<OrganizationDto?> GetOrganizationAsync(Guid id);
        Task<OrganizationDto> CreateOrganizationAsync(CreateOrganizationDto createDto);
        Task<OrganizationDto> UpdateOrganizationAsync(Guid id, UpdateOrganizationDto updateDto);
        Task DeleteOrganizationAsync(Guid id);
    }

    public class OrganizationService : IOrganizationService
    {
        private readonly HttpClient _httpClient;

        public OrganizationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<OrganizationDto>> GetOrganizationsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<OrganizationDto>>("api/organizations");
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
                var url = $"api/organizations/{id}";
                Console.WriteLine($"Making request to: {url}");
                Console.WriteLine($"HttpClient BaseAddress: {_httpClient.BaseAddress}");
                
                var response = await _httpClient.GetFromJsonAsync<OrganizationDto>(url);
                Console.WriteLine($"Response received: {response?.OrganizationName ?? "null"}");
                
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching organization {id}: {ex.Message}");
                Console.WriteLine($"Exception details: {ex}");
                return null;
            }
        }

        public async Task<OrganizationDto> CreateOrganizationAsync(CreateOrganizationDto createDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/organizations", createDto);
                response.EnsureSuccessStatusCode();
                
                var result = await response.Content.ReadFromJsonAsync<OrganizationDto>();
                return result ?? throw new InvalidOperationException("Failed to deserialize created organization");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating organization: {ex.Message}");
                throw;
            }
        }

        public async Task<OrganizationDto> UpdateOrganizationAsync(Guid id, UpdateOrganizationDto updateDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/organizations/{id}", updateDto);
                response.EnsureSuccessStatusCode();
                
                var result = await response.Content.ReadFromJsonAsync<OrganizationDto>();
                return result ?? throw new InvalidOperationException("Failed to deserialize updated organization");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating organization {id}: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteOrganizationAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/organizations/{id}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting organization {id}: {ex.Message}");
                throw;
            }
        }
    }
}
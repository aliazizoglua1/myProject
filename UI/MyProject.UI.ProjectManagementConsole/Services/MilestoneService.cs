using System.Net.Http.Json;
using MyProject.UI.ProjectManagementConsole.Models;

namespace MyProject.UI.ProjectManagementConsole.Services
{
    public interface IMilestoneService
    {
        Task<List<MilestoneDto>> GetMilestonesAsync();
        Task<MilestoneDto?> GetMilestoneAsync(Guid id);
        Task<MilestoneDto> CreateMilestoneAsync(CreateMilestoneDto milestone);
        Task<MilestoneDto> UpdateMilestoneAsync(Guid id, UpdateMilestoneDto milestone);
        Task<bool> DeleteMilestoneAsync(Guid id);
        Task<List<MilestoneDto>> GetByProjectIdAsync(Guid projectId);
        Task<List<MilestoneDto>> GetByTenantIdAsync(Guid tenantId);
        Task<List<MilestoneDto>> GetAchievedMilestonesAsync();
        Task<List<MilestoneDto>> GetPendingMilestonesAsync();
        Task<List<MilestoneDto>> GetOverdueMilestonesAsync();
        Task<bool> ExistsByNameInProjectAsync(Guid projectId, string name);
        Task<MilestoneDto> MarkMilestoneAchievedAsync(Guid id, MarkMilestoneAchievedDto dto);
    }

    public class MilestoneService : IMilestoneService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7001"; // Update this to match your API URL

        public MilestoneService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<MilestoneDto>> GetMilestonesAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<MilestoneDto>>($"{_baseUrl}/api/milestones");
                return response ?? new List<MilestoneDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching milestones: {ex.Message}");
                return new List<MilestoneDto>();
            }
        }

        public async Task<MilestoneDto?> GetMilestoneAsync(Guid id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<MilestoneDto>($"{_baseUrl}/api/milestones/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching milestone: {ex.Message}");
                return null;
            }
        }

        public async Task<MilestoneDto> CreateMilestoneAsync(CreateMilestoneDto milestone)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/milestones", milestone);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<MilestoneDto>() ?? new MilestoneDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating milestone: {ex.Message}");
                throw;
            }
        }

        public async Task<MilestoneDto> UpdateMilestoneAsync(Guid id, UpdateMilestoneDto milestone)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/milestones/{id}", milestone);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<MilestoneDto>() ?? new MilestoneDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating milestone: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteMilestoneAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/milestones/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting milestone: {ex.Message}");
                return false;
            }
        }

        public async Task<List<MilestoneDto>> GetByProjectIdAsync(Guid projectId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<MilestoneDto>>($"{_baseUrl}/api/milestones/project/{projectId}");
                return response ?? new List<MilestoneDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching milestones by project: {ex.Message}");
                return new List<MilestoneDto>();
            }
        }

        public async Task<List<MilestoneDto>> GetByTenantIdAsync(Guid tenantId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<MilestoneDto>>($"{_baseUrl}/api/milestones/tenant/{tenantId}");
                return response ?? new List<MilestoneDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching milestones by tenant: {ex.Message}");
                return new List<MilestoneDto>();
            }
        }

        public async Task<List<MilestoneDto>> GetAchievedMilestonesAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<MilestoneDto>>($"{_baseUrl}/api/milestones/achieved");
                return response ?? new List<MilestoneDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching achieved milestones: {ex.Message}");
                return new List<MilestoneDto>();
            }
        }

        public async Task<List<MilestoneDto>> GetPendingMilestonesAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<MilestoneDto>>($"{_baseUrl}/api/milestones/pending");
                return response ?? new List<MilestoneDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching pending milestones: {ex.Message}");
                return new List<MilestoneDto>();
            }
        }

        public async Task<List<MilestoneDto>> GetOverdueMilestonesAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<MilestoneDto>>($"{_baseUrl}/api/milestones/overdue");
                return response ?? new List<MilestoneDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching overdue milestones: {ex.Message}");
                return new List<MilestoneDto>();
            }
        }

        public async Task<bool> ExistsByNameInProjectAsync(Guid projectId, string name)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/milestones/exists/project/{projectId}/name/{name}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking milestone existence: {ex.Message}");
                return false;
            }
        }

        public async Task<MilestoneDto> MarkMilestoneAchievedAsync(Guid id, MarkMilestoneAchievedDto dto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/milestones/{id}/mark-achieved", dto);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<MilestoneDto>() ?? new MilestoneDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error marking milestone as achieved: {ex.Message}");
                throw;
            }
        }
    }
} 
using System.Net.Http.Json;
using MyProject.UI.ProjectManagementConsole.Models;

namespace MyProject.UI.ProjectManagementConsole.Services
{
    public interface ITaskService
    {
        Task<List<TaskDto>> GetTasksAsync();
        Task<TaskDto?> GetTaskAsync(Guid id);
        Task<TaskDto> CreateTaskAsync(CreateTaskDto task);
        Task<TaskDto> UpdateTaskAsync(Guid id, UpdateTaskDto task);
        Task<bool> DeleteTaskAsync(Guid id);
        Task<List<TaskDto>> GetByTenantAsync(Guid tenantId);
        Task<List<TaskDto>> GetByTenantAndStatusAsync(Guid tenantId, string status);
        Task<List<TaskDto>> GetByTenantAndPriorityAsync(Guid tenantId, string priority);
        Task<List<TaskDto>> GetByProjectAndStatusAsync(Guid projectId, string status);
        Task<List<TaskDto>> GetByMilestoneIdAsync(Guid milestoneId);
        Task<List<TaskDto>> GetByTenantAndUserAsync(Guid tenantId, Guid userId);
        Task<List<TaskDto>> GetOverdueTasksByTenantAsync(Guid tenantId);
        Task<List<TaskDto>> GetMilestonesByTenantAsync(Guid tenantId);
    }

    public class TaskService : ITaskService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7001"; // Update this to match your API URL

        public TaskService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TaskDto>> GetTasksAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<TaskDto>>($"{_baseUrl}/api/tasks");
                return response ?? new List<TaskDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching tasks: {ex.Message}");
                return new List<TaskDto>();
            }
        }

        public async Task<TaskDto?> GetTaskAsync(Guid id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<TaskDto>($"{_baseUrl}/api/tasks/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching task: {ex.Message}");
                return null;
            }
        }

        public async Task<TaskDto> CreateTaskAsync(CreateTaskDto task)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/tasks", task);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<TaskDto>() ?? new TaskDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating task: {ex.Message}");
                throw;
            }
        }

        public async Task<TaskDto> UpdateTaskAsync(Guid id, UpdateTaskDto task)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/tasks/{id}", task);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<TaskDto>() ?? new TaskDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating task: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/tasks/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting task: {ex.Message}");
                return false;
            }
        }

        public async Task<List<TaskDto>> GetByTenantAsync(Guid tenantId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<TaskDto>>($"{_baseUrl}/api/tasks/tenant/{tenantId}");
                return response ?? new List<TaskDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching tasks by tenant: {ex.Message}");
                return new List<TaskDto>();
            }
        }

        public async Task<List<TaskDto>> GetByTenantAndStatusAsync(Guid tenantId, string status)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<TaskDto>>($"{_baseUrl}/api/tasks/tenant/{tenantId}/status/{status}");
                return response ?? new List<TaskDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching tasks by tenant and status: {ex.Message}");
                return new List<TaskDto>();
            }
        }

        public async Task<List<TaskDto>> GetByTenantAndPriorityAsync(Guid tenantId, string priority)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<TaskDto>>($"{_baseUrl}/api/tasks/tenant/{tenantId}/priority/{priority}");
                return response ?? new List<TaskDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching tasks by tenant and priority: {ex.Message}");
                return new List<TaskDto>();
            }
        }

        public async Task<List<TaskDto>> GetByProjectAndStatusAsync(Guid projectId, string status)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<TaskDto>>($"{_baseUrl}/api/tasks/project/{projectId}/status/{status}");
                return response ?? new List<TaskDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching tasks by project and status: {ex.Message}");
                return new List<TaskDto>();
            }
        }

        public async Task<List<TaskDto>> GetByMilestoneIdAsync(Guid milestoneId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<TaskDto>>($"{_baseUrl}/api/tasks/milestone/{milestoneId}");
                return response ?? new List<TaskDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching tasks by milestone: {ex.Message}");
                return new List<TaskDto>();
            }
        }

        public async Task<List<TaskDto>> GetByTenantAndUserAsync(Guid tenantId, Guid userId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<TaskDto>>($"{_baseUrl}/api/tasks/tenant/{tenantId}/user/{userId}");
                return response ?? new List<TaskDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching tasks by tenant and user: {ex.Message}");
                return new List<TaskDto>();
            }
        }

        public async Task<List<TaskDto>> GetOverdueTasksByTenantAsync(Guid tenantId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<TaskDto>>($"{_baseUrl}/api/tasks/tenant/{tenantId}/overdue");
                return response ?? new List<TaskDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching overdue tasks by tenant: {ex.Message}");
                return new List<TaskDto>();
            }
        }

        public async Task<List<TaskDto>> GetMilestonesByTenantAsync(Guid tenantId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<TaskDto>>($"{_baseUrl}/api/tasks/tenant/{tenantId}/milestones");
                return response ?? new List<TaskDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching milestone tasks by tenant: {ex.Message}");
                return new List<TaskDto>();
            }
        }
    }
} 
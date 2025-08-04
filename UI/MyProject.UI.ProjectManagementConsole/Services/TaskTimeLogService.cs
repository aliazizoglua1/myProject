using System.Net.Http.Json;
using MyProject.UI.ProjectManagementConsole.Models;

namespace MyProject.UI.ProjectManagementConsole.Services
{
    public interface ITaskTimeLogService
    {
        Task<List<TaskTimeLogDto>> GetTaskTimeLogsAsync();
        Task<TaskTimeLogDto?> GetTaskTimeLogAsync(Guid id);
        Task<List<TaskTimeLogDto>> GetByTaskAsync(Guid taskId);
        Task<List<TaskTimeLogDto>> GetByResourceAsync(Guid resourceId);
        Task<List<TaskTimeLogDto>> GetByTenantAsync(Guid tenantId);
        Task<List<TaskTimeLogDto>> GetByDateRangeAsync(DateOnly startDate, DateOnly endDate);
        Task<List<TaskTimeLogDto>> GetByTaskAndDateRangeAsync(Guid taskId, DateOnly startDate, DateOnly endDate);
        Task<List<TaskTimeLogDto>> GetByResourceAndDateRangeAsync(Guid resourceId, DateOnly startDate, DateOnly endDate);
        Task<decimal> GetTotalHoursByTaskAsync(Guid taskId);
        Task<decimal> GetTotalHoursByResourceAsync(Guid resourceId);
        Task<decimal> GetBillableHoursByTaskAsync(Guid taskId);
        Task<decimal> GetBillableHoursByResourceAsync(Guid resourceId);
        Task<decimal> GetTotalCostByTaskAsync(Guid taskId);
        Task<decimal> GetTotalCostByResourceAsync(Guid resourceId);
        Task<TaskTimeLogDto> CreateTaskTimeLogAsync(CreateTaskTimeLogDto taskTimeLog);
        Task<TaskTimeLogDto> UpdateTaskTimeLogAsync(Guid id, UpdateTaskTimeLogDto taskTimeLog);
        Task<bool> DeleteTaskTimeLogAsync(Guid id);
    }

    public class TaskTimeLogService : ITaskTimeLogService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7001"; // Update this to match your API URL

        public TaskTimeLogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TaskTimeLogDto>> GetTaskTimeLogsAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<TaskTimeLogDto>>($"{_baseUrl}/api/tasktimelogs");
                return response ?? new List<TaskTimeLogDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching task time logs: {ex.Message}");
                return new List<TaskTimeLogDto>();
            }
        }

        public async Task<TaskTimeLogDto?> GetTaskTimeLogAsync(Guid id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<TaskTimeLogDto>($"{_baseUrl}/api/tasktimelogs/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching task time log: {ex.Message}");
                return null;
            }
        }

        public async Task<List<TaskTimeLogDto>> GetByTaskAsync(Guid taskId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<TaskTimeLogDto>>($"{_baseUrl}/api/tasktimelogs/task/{taskId}");
                return response ?? new List<TaskTimeLogDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching task time logs by task: {ex.Message}");
                return new List<TaskTimeLogDto>();
            }
        }

        public async Task<List<TaskTimeLogDto>> GetByResourceAsync(Guid resourceId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<TaskTimeLogDto>>($"{_baseUrl}/api/tasktimelogs/resource/{resourceId}");
                return response ?? new List<TaskTimeLogDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching task time logs by resource: {ex.Message}");
                return new List<TaskTimeLogDto>();
            }
        }

        public async Task<List<TaskTimeLogDto>> GetByTenantAsync(Guid tenantId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<TaskTimeLogDto>>($"{_baseUrl}/api/tasktimelogs/tenant/{tenantId}");
                return response ?? new List<TaskTimeLogDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching task time logs by tenant: {ex.Message}");
                return new List<TaskTimeLogDto>();
            }
        }

        public async Task<List<TaskTimeLogDto>> GetByDateRangeAsync(DateOnly startDate, DateOnly endDate)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<TaskTimeLogDto>>($"{_baseUrl}/api/tasktimelogs/daterange?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
                return response ?? new List<TaskTimeLogDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching task time logs by date range: {ex.Message}");
                return new List<TaskTimeLogDto>();
            }
        }

        public async Task<List<TaskTimeLogDto>> GetByTaskAndDateRangeAsync(Guid taskId, DateOnly startDate, DateOnly endDate)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<TaskTimeLogDto>>($"{_baseUrl}/api/tasktimelogs/task/{taskId}/daterange?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
                return response ?? new List<TaskTimeLogDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching task time logs by task and date range: {ex.Message}");
                return new List<TaskTimeLogDto>();
            }
        }

        public async Task<List<TaskTimeLogDto>> GetByResourceAndDateRangeAsync(Guid resourceId, DateOnly startDate, DateOnly endDate)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<TaskTimeLogDto>>($"{_baseUrl}/api/tasktimelogs/resource/{resourceId}/daterange?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
                return response ?? new List<TaskTimeLogDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching task time logs by resource and date range: {ex.Message}");
                return new List<TaskTimeLogDto>();
            }
        }

        public async Task<decimal> GetTotalHoursByTaskAsync(Guid taskId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<decimal>($"{_baseUrl}/api/tasktimelogs/task/{taskId}/total-hours");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching total hours by task: {ex.Message}");
                return 0;
            }
        }

        public async Task<decimal> GetTotalHoursByResourceAsync(Guid resourceId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<decimal>($"{_baseUrl}/api/tasktimelogs/resource/{resourceId}/total-hours");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching total hours by resource: {ex.Message}");
                return 0;
            }
        }

        public async Task<decimal> GetBillableHoursByTaskAsync(Guid taskId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<decimal>($"{_baseUrl}/api/tasktimelogs/task/{taskId}/billable-hours");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching billable hours by task: {ex.Message}");
                return 0;
            }
        }

        public async Task<decimal> GetBillableHoursByResourceAsync(Guid resourceId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<decimal>($"{_baseUrl}/api/tasktimelogs/resource/{resourceId}/billable-hours");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching billable hours by resource: {ex.Message}");
                return 0;
            }
        }

        public async Task<decimal> GetTotalCostByTaskAsync(Guid taskId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<decimal>($"{_baseUrl}/api/tasktimelogs/task/{taskId}/total-cost");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching total cost by task: {ex.Message}");
                return 0;
            }
        }

        public async Task<decimal> GetTotalCostByResourceAsync(Guid resourceId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<decimal>($"{_baseUrl}/api/tasktimelogs/resource/{resourceId}/total-cost");
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching total cost by resource: {ex.Message}");
                return 0;
            }
        }

        public async Task<TaskTimeLogDto> CreateTaskTimeLogAsync(CreateTaskTimeLogDto taskTimeLog)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/tasktimelogs", taskTimeLog);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<TaskTimeLogDto>() ?? new TaskTimeLogDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating task time log: {ex.Message}");
                throw;
            }
        }

        public async Task<TaskTimeLogDto> UpdateTaskTimeLogAsync(Guid id, UpdateTaskTimeLogDto taskTimeLog)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/tasktimelogs/{id}", taskTimeLog);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<TaskTimeLogDto>() ?? new TaskTimeLogDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating task time log: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteTaskTimeLogAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/tasktimelogs/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting task time log: {ex.Message}");
                return false;
            }
        }
    }
} 
using MyProject.UI.RazorPages.Models;
using System.Text.Json;
using System.Text;

namespace MyProject.UI.RazorPages.Services
{
    public class TaskService : ITaskService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public TaskService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            var response = await _httpClient.GetAsync("api/tasks");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var tasks = JsonSerializer.Deserialize<IEnumerable<TaskDto>>(json, _jsonOptions);
            return tasks ?? new List<TaskDto>();
        }

        public async Task<TaskDto?> GetTaskAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/tasks/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TaskDto>(json, _jsonOptions);
        }

        public async Task<TaskDto> CreateTaskAsync(CreateTaskDto task)
        {
            var json = JsonSerializer.Serialize(task, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync("api/tasks", content);
            response.EnsureSuccessStatusCode();
            
            var responseJson = await response.Content.ReadAsStringAsync();
            var createdTask = JsonSerializer.Deserialize<TaskDto>(responseJson, _jsonOptions);
            return createdTask ?? throw new InvalidOperationException("Failed to create task");
        }

        public async Task<TaskDto> UpdateTaskAsync(Guid id, UpdateTaskDto task)
        {
            var json = JsonSerializer.Serialize(task, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PutAsync($"api/tasks/{id}", content);
            response.EnsureSuccessStatusCode();
            
            var responseJson = await response.Content.ReadAsStringAsync();
            var updatedTask = JsonSerializer.Deserialize<TaskDto>(responseJson, _jsonOptions);
            return updatedTask ?? throw new InvalidOperationException("Failed to update task");
        }

        public async Task<bool> DeleteTaskAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/tasks/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<TaskDto>> GetTasksByProjectAsync(Guid projectId)
        {
            var response = await _httpClient.GetAsync($"api/tasks/project/{projectId}");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var tasks = JsonSerializer.Deserialize<IEnumerable<TaskDto>>(json, _jsonOptions);
            return tasks ?? new List<TaskDto>();
        }

        public async Task<IEnumerable<TaskDto>> GetTasksByStatusAsync(string status)
        {
            var response = await _httpClient.GetAsync($"api/tasks/status/{status}");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var tasks = JsonSerializer.Deserialize<IEnumerable<TaskDto>>(json, _jsonOptions);
            return tasks ?? new List<TaskDto>();
        }

        public async Task<IEnumerable<TaskDto>> GetTasksByPriorityAsync(string priority)
        {
            var response = await _httpClient.GetAsync($"api/tasks/priority/{priority}");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var tasks = JsonSerializer.Deserialize<IEnumerable<TaskDto>>(json, _jsonOptions);
            return tasks ?? new List<TaskDto>();
        }

        public async Task<IEnumerable<TaskDto>> GetTasksByAssignedUserAsync(Guid userId)
        {
            var response = await _httpClient.GetAsync($"api/tasks/assigned/{userId}");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var tasks = JsonSerializer.Deserialize<IEnumerable<TaskDto>>(json, _jsonOptions);
            return tasks ?? new List<TaskDto>();
        }

        public async Task<IEnumerable<TaskDto>> GetSubtasksAsync(Guid parentTaskId)
        {
            var response = await _httpClient.GetAsync($"api/tasks/parent/{parentTaskId}");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var tasks = JsonSerializer.Deserialize<IEnumerable<TaskDto>>(json, _jsonOptions);
            return tasks ?? new List<TaskDto>();
        }

        public async Task<IEnumerable<TaskDto>> GetMilestonesAsync()
        {
            var response = await _httpClient.GetAsync("api/tasks/milestones");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var tasks = JsonSerializer.Deserialize<IEnumerable<TaskDto>>(json, _jsonOptions);
            return tasks ?? new List<TaskDto>();
        }

        public async Task<IEnumerable<TaskDto>> GetOverdueTasksAsync()
        {
            var response = await _httpClient.GetAsync("api/tasks/overdue");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var tasks = JsonSerializer.Deserialize<IEnumerable<TaskDto>>(json, _jsonOptions);
            return tasks ?? new List<TaskDto>();
        }

        public async Task<IEnumerable<TaskDto>> GetTasksWithSubtasksAsync()
        {
            var response = await _httpClient.GetAsync("api/tasks/with-subtasks");
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var tasks = JsonSerializer.Deserialize<IEnumerable<TaskDto>>(json, _jsonOptions);
            return tasks ?? new List<TaskDto>();
        }
    }
}
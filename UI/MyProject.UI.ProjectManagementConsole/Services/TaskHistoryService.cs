using System.Net.Http.Json;
using MyProject.UI.ProjectManagementConsole.Models;

namespace MyProject.UI.ProjectManagementConsole.Services;

public class TaskHistoryService : ITaskHistoryService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "api/taskhistories";

    public TaskHistoryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<TaskHistoryDto>> GetTaskHistoriesAsync()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<TaskHistoryDto>>(_baseUrl) ?? new List<TaskHistoryDto>();
    }

    public async Task<TaskHistoryDto?> GetTaskHistoryAsync(Guid taskHistoryId)
    {
        return await _httpClient.GetFromJsonAsync<TaskHistoryDto>($"{_baseUrl}/{taskHistoryId}");
    }

    public async Task<IEnumerable<TaskHistoryDto>> GetByTaskIdAsync(Guid taskId)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<TaskHistoryDto>>($"{_baseUrl}/task/{taskId}") ?? new List<TaskHistoryDto>();
    }

    public async Task<IEnumerable<TaskHistoryDto>> GetByUserIdAsync(Guid userId)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<TaskHistoryDto>>($"{_baseUrl}/user/{userId}") ?? new List<TaskHistoryDto>();
    }

    public async Task<IEnumerable<TaskHistoryDto>> GetByTenantIdAsync(Guid tenantId)
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<TaskHistoryDto>>($"{_baseUrl}/tenant/{tenantId}") ?? new List<TaskHistoryDto>();
    }

    public async Task<TaskHistoryDto> CreateTaskHistoryAsync(CreateTaskHistoryDto createDto)
    {
        createDto.OrganizationId =  Guid.Parse("de730e94-c9dd-4b5f-91d3-75c6dd9f2f43");
        createDto.UserId = Guid.Parse("793be772-2218-4b20-86a6-6fd6d930f37e");
        var response = await _httpClient.PostAsJsonAsync(_baseUrl, createDto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TaskHistoryDto>() ?? throw new InvalidOperationException("Failed to deserialize response");
    }

    public async Task<TaskHistoryDto> UpdateTaskHistoryAsync(Guid taskHistoryId, UpdateTaskHistoryDto updateDto)
    {
        var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/{taskHistoryId}", updateDto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TaskHistoryDto>() ?? throw new InvalidOperationException("Failed to deserialize response");
    }

    public async Task<bool> DeleteTaskHistoryAsync(Guid taskHistoryId)
    {
        var response = await _httpClient.DeleteAsync($"{_baseUrl}/{taskHistoryId}");
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteByTaskIdAsync(Guid taskId)
    {
        var response = await _httpClient.DeleteAsync($"{_baseUrl}/task/{taskId}");
        return response.IsSuccessStatusCode;
    }
} 
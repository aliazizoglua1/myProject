using System.Net.Http.Json;
using MyProject.UI.ProjectManagementConsole.Models;

namespace MyProject.UI.ProjectManagementConsole.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUsersAsync();
        Task<UserDto?> GetUserAsync(Guid id);
        Task<UserDto> CreateUserAsync(CreateUserDto user);
        Task<UserDto> UpdateUserAsync(Guid id, UpdateUserDto user);
        Task<bool> DeleteUserAsync(Guid id);
        Task<UserDto?> GetUserByEmailAsync(string email);
        Task<List<UserDto>> GetActiveUsersAsync();
        Task<List<UserDto>> GetInactiveUsersAsync();
        Task<bool> IsEmailUniqueAsync(string email);
    }

    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7001"; // Update this to match your API URL

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<UserDto>>($"{_baseUrl}/api/users");
                return response ?? new List<UserDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching users: {ex.Message}");
                return new List<UserDto>();
            }
        }

        public async Task<UserDto?> GetUserAsync(Guid id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<UserDto>($"{_baseUrl}/api/users/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user: {ex.Message}");
                return null;
            }
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto user)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/users", user);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<UserDto>() ?? new UserDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating user: {ex.Message}");
                throw;
            }
        }

        public async Task<UserDto> UpdateUserAsync(Guid id, UpdateUserDto user)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/users/{id}", user);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<UserDto>() ?? new UserDto();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating user: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/users/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting user: {ex.Message}");
                return false;
            }
        }

        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<UserDto>($"{_baseUrl}/api/users/email/{email}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user by email: {ex.Message}");
                return null;
            }
        }

        public async Task<List<UserDto>> GetActiveUsersAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<UserDto>>($"{_baseUrl}/api/users/active");
                return response ?? new List<UserDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching active users: {ex.Message}");
                return new List<UserDto>();
            }
        }

        public async Task<List<UserDto>> GetInactiveUsersAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<UserDto>>($"{_baseUrl}/api/users/inactive");
                return response ?? new List<UserDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching inactive users: {ex.Message}");
                return new List<UserDto>();
            }
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/api/users/email-unique/{email}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking email uniqueness: {ex.Message}");
                return false;
            }
        }
    }
} 
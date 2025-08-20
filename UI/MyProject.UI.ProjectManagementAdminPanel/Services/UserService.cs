using System.Net.Http.Json;
using MyProject.UI.ProjectManagementAdminPanel.Models;

namespace MyProject.UI.ProjectManagementAdminPanel.Services
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUsersAsync();
        Task<UserDto?> GetUserAsync(Guid id);
        Task<UserDto> CreateUserAsync(CreateUserDto createDto);
        Task<List<OrganizationUserWithDetailsDto>> GetOrganizationUsersAsync(Guid organizationId);
        Task<OrganizationUserDto> AddUserToOrganizationAsync(Guid organizationId, CreateOrganizationUserDto createDto);
        Task<UserDto> CreateUserAndAddToOrganizationAsync(Guid organizationId, AddUserToOrganizationDto addUserDto);
        Task RemoveUserFromOrganizationAsync(Guid organizationId, Guid userId);
    }

    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<UserDto>> GetUsersAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<UserDto>>("api/users");
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
                return await _httpClient.GetFromJsonAsync<UserDto>($"api/users/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user {id}: {ex.Message}");
                return null;
            }
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto createDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/users", createDto);
                response.EnsureSuccessStatusCode();
                
                var result = await response.Content.ReadFromJsonAsync<UserDto>();
                return result ?? throw new InvalidOperationException("Failed to deserialize created user");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating user: {ex.Message}");
                throw;
            }
        }

        public async Task<List<OrganizationUserWithDetailsDto>> GetOrganizationUsersAsync(Guid organizationId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<OrganizationUserWithDetailsDto>>($"api/organizations/{organizationId}/users");
                return response ?? new List<OrganizationUserWithDetailsDto>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching organization users: {ex.Message}");
                return new List<OrganizationUserWithDetailsDto>();
            }
        }

        public async Task<OrganizationUserDto> AddUserToOrganizationAsync(Guid organizationId, CreateOrganizationUserDto createDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/organizationusers", createDto);
                response.EnsureSuccessStatusCode();
                
                var result = await response.Content.ReadFromJsonAsync<OrganizationUserDto>();
                return result ?? throw new InvalidOperationException("Failed to deserialize organization user");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding user to organization: {ex.Message}");
                throw;
            }
        }

        public async Task<UserDto> CreateUserAndAddToOrganizationAsync(Guid organizationId, AddUserToOrganizationDto addUserDto)
        {
            try
            {
                // First, create the user
                var createUserDto = new CreateUserDto
                {
                    Email = addUserDto.Email,
                    FirstName = addUserDto.FirstName,
                    LastName = addUserDto.LastName,
                    Password = addUserDto.Password,
                    IsActive = addUserDto.IsActive
                };

                var user = await CreateUserAsync(createUserDto);

                // Then, add the user to the organization
                var createOrgUserDto = new CreateOrganizationUserDto
                {
                    OrganizationId = organizationId,
                    UserId = user.UserId,
                    Role = addUserDto.Role,
                    IsActive = addUserDto.IsActive
                };

                await AddUserToOrganizationAsync(organizationId, createOrgUserDto);

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating user and adding to organization: {ex.Message}");
                throw;
            }
        }

        public async Task RemoveUserFromOrganizationAsync(Guid organizationId, Guid userId)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/organizationusers/{organizationId}/{userId}");
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing user from organization: {ex.Message}");
                throw;
            }
        }
    }
}
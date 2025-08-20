using MyProject.UI.ProjectManagementConsole.Models;

namespace MyProject.UI.ProjectManagementConsole.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> LoginAsync(string email, string password);
        Task LogoutAsync();
        Task<bool> IsAuthenticatedAsync();
        Task<string?> GetTokenAsync();
        Task<UserDto?> GetCurrentUserAsync();
        event Action<bool> AuthenticationStateChanged;
    }

    public class AuthenticationResult
    {
        public bool Succeeded { get; set; }
        public string Token { get; set; } = string.Empty;
        public UserDto? User { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
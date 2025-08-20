using Microsoft.JSInterop;
using MyProject.UI.ProjectManagementConsole.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace MyProject.UI.ProjectManagementConsole.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private const string TOKEN_KEY = "authToken";
        private const string USER_KEY = "currentUser";
        private const string EXPIRES_KEY = "tokenExpires";

        public event Action<bool> AuthenticationStateChanged = delegate { };

        public AuthenticationService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            try
            {
                var loginRequest = new
                {
                    Email = email,
                    Password = password
                };

                var response = await _httpClient.PostAsJsonAsync("api/users/login", loginRequest);
                
                if (response.IsSuccessStatusCode)
                {
                    var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponseDto>();
                    if (loginResponse != null)
                    {
                        // Store token and user info in local storage
                        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", TOKEN_KEY, loginResponse.Token);
                        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", USER_KEY, JsonSerializer.Serialize(loginResponse.User));
                        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", EXPIRES_KEY, loginResponse.ExpiresAt.ToString("O"));

                        // Set authorization header for future requests
                        _httpClient.DefaultRequestHeaders.Authorization = 
                            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", loginResponse.Token);

                        AuthenticationStateChanged.Invoke(true);

                        return new AuthenticationResult
                        {
                            Succeeded = true,
                            Token = loginResponse.Token,
                            User = loginResponse.User,
                            ExpiresAt = loginResponse.ExpiresAt
                        };
                    }
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                return new AuthenticationResult
                {
                    Succeeded = false,
                    ErrorMessage = errorContent.Length > 0 ? errorContent : "Login failed"
                };
            }
            catch (Exception ex)
            {
                return new AuthenticationResult
                {
                    Succeeded = false,
                    ErrorMessage = $"Login error: {ex.Message}"
                };
            }
        }

        public async Task LogoutAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", TOKEN_KEY);
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", USER_KEY);
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", EXPIRES_KEY);

            _httpClient.DefaultRequestHeaders.Authorization = null;
            AuthenticationStateChanged.Invoke(false);
        }

        public async Task<bool> IsAuthenticatedAsync()
        {
            try
            {
                var token = await GetTokenAsync();
                if (string.IsNullOrEmpty(token))
                    return false;

                var expiresString = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", EXPIRES_KEY);
                if (string.IsNullOrEmpty(expiresString))
                    return false;

                if (DateTime.TryParse(expiresString, out DateTime expires))
                {
                    if (expires <= DateTime.UtcNow)
                    {
                        await LogoutAsync();
                        return false;
                    }
                }

                // Set authorization header if not already set
                if (_httpClient.DefaultRequestHeaders.Authorization == null)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = 
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<string?> GetTokenAsync()
        {
            try
            {
                return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TOKEN_KEY);
            }
            catch
            {
                return null;
            }
        }

        public async Task<UserDto?> GetCurrentUserAsync()
        {
            try
            {
                var userJson = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", USER_KEY);
                if (string.IsNullOrEmpty(userJson))
                    return null;

                return JsonSerializer.Deserialize<UserDto>(userJson);
            }
            catch
            {
                return null;
            }
        }
    }

    public class LoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public UserDto User { get; set; } = new();
        public DateTime ExpiresAt { get; set; }
    }
}
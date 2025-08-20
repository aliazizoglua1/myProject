using Microsoft.JSInterop;
using System.Net.Http.Headers;

namespace MyProject.UI.ProjectManagementConsole.Services
{
    public class AuthenticatedHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private const string TOKEN_KEY = "authToken";

        public AuthenticatedHttpClient(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
        }

        public async Task<HttpClient> GetHttpClientAsync()
        {
            await EnsureAuthenticatedAsync();
            return _httpClient;
        }

        private async Task EnsureAuthenticatedAsync()
        {
            try
            {
                var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", TOKEN_KEY);
                
                if (!string.IsNullOrEmpty(token))
                {
                    _httpClient.DefaultRequestHeaders.Authorization = 
                        new AuthenticationHeaderValue("Bearer", token);
                }
                else
                {
                    _httpClient.DefaultRequestHeaders.Authorization = null;
                }
            }
            catch (Exception)
            {
                // Handle JS interop exceptions (e.g., during prerendering)
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }
    }
}
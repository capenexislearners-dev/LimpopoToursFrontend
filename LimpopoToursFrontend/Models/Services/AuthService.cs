using LimpopoToursFrontend.Models;
using System.Net.Http.Json;

namespace LimpopoToursFrontend.Services
{
    public class AuthService
    {
        private readonly HttpClient _http;

        public AuthService(HttpClient http)
        {
            _http = http;
        }

        public string? Token { get; private set; }
        public string? FullName { get; private set; }
        public string? Email { get; private set; }
        public bool IsLoggedIn => !string.IsNullOrEmpty(Token);

        public async Task<string?> Register(RegisterRequest request)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/Auth/register", request);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
                    Token = result?.Token;
                    FullName = result?.FullName;
                    Email = result?.Email;
                    return null; // no error
                }
                var error = await response.Content.ReadAsStringAsync();
                return error;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string?> Login(LoginRequest request)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("api/Auth/login", request);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
                    Token = result?.Token;
                    FullName = result?.FullName;
                    Email = result?.Email;
                    return null; // no error
                }
                return "Invalid email or password";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public void Logout()
        {
            Token = null;
            FullName = null;
            Email = null;
        }
    }
}
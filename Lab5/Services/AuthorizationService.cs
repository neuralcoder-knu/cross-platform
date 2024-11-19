using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Lab5.Services;

public class AuthorizationService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public AuthorizationService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    private async Task<string?> CreateToken()
    {
        var domain = _configuration["Auth0:Domain"];
        var clientId = _configuration["Auth0:ClientId"];
        var clientSecret = _configuration["Auth0:ClientSecret"];

        var content = new StringContent(JsonSerializer.Serialize(new
        {
            client_id = clientId,
            client_secret = clientSecret,
            audience = $"https://{domain}/api/v2/",
            grant_type = "client_credentials"
        }), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"https://{domain}/oauth/token", content);
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);
        return tokenResponse.GetProperty("access_token").GetString();
    }

    public async Task<bool> RegisterUserAsync(string email, string password,
        string fullName, string phoneNumber,
        string username)
    {
        var token = await CreateToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var domain = _configuration["Auth0:Domain"];
        
        var content = new StringContent(JsonSerializer.Serialize(new
        {
            email,
            password,
            connection = "Username-Password-Authentication",
            nickname = username,
            given_name = fullName,
            user_metadata = new {
                phone = phoneNumber
            }
        }), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"https://{domain}/api/v2/users", content);
        return response.IsSuccessStatusCode;
    }
    
    public async Task<string?> LoginUserAsync(string email, string password)
    {
        var domain = _configuration["Auth0:Domain"];
        var clientId = _configuration["Auth0:ClientId"];
        var clientSecret = _configuration["Auth0:ClientSecret"];

        var content = new StringContent(JsonSerializer.Serialize(new
        {
            grant_type = "password",
            username = email,
            password,
            audience = $"https://{domain}/api/v2/",
            client_id = clientId,
            client_secret = clientSecret,
            scope = "openid profile email"
        }), Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync($"https://{domain}/oauth/token", content);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);
        return tokenResponse.GetProperty("access_token").GetString();
    }
    
    public async Task<JsonElement?> GetUserInfoAsync(string accessToken)
    {
        var domain = _configuration["Auth0:Domain"];

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await _httpClient.GetAsync($"https://{domain}/userinfo");

        if (!response.IsSuccessStatusCode)
            return null;

        var responseContent = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<JsonElement>(responseContent);
    }

}
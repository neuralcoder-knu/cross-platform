using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Lab5.Models;

namespace Lab5.Services;

public class Lab6ApiService
{
    
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    private readonly string? apiHost;

    public Lab6ApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        apiHost = configuration["Lab6ApiHost"];
    }

    public async Task<string?> GetAccessToken()
    {
        var domain = _configuration["Auth0:Domain"];
        var clientId = _configuration["Auth0:ClientId"];
        var clientSecret = _configuration["Auth0:ClientSecret"];
        var audience = _configuration["Auth0:ApiAudience"];
        
        var tokenRequest = new HttpRequestMessage(HttpMethod.Post, $"https://{domain}/oauth/token")
        {
            Content = new StringContent(JsonSerializer.Serialize(new
            {
                client_id = clientId,
                client_secret = clientSecret,
                audience,
                grant_type = "client_credentials"
            }), Encoding.UTF8, "application/json")
        };

        var response = await _httpClient.SendAsync(tokenRequest);

        var responseContent = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);
        return tokenResponse.GetProperty("access_token").GetString();
    }
    
    public async Task<List<RefCurrencyCode>> GetCurrenciesAsync()
    {
        var token = await GetAccessToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var response = await _httpClient.GetAsync($"{apiHost}Reference/Currencies");
        response.EnsureSuccessStatusCode();
        var res = await response.Content.ReadFromJsonAsync<Response<RefCurrencyCode>>();
        return res.Result;
    }
    
    public async Task<List<Cardholder>> GetCardholdersAsync()
    {
        var token = await GetAccessToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var response = await _httpClient.GetAsync($"{apiHost}Cardholders");
        response.EnsureSuccessStatusCode();
        var res = await response.Content.ReadFromJsonAsync<Response<Cardholder>>();
        return res.Result;
    }
    
    public async Task<Cardholder> GetCarholder(int CurrencyCode)
    {
        var token = await GetAccessToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var response = await _httpClient.GetAsync($"{apiHost}Cardholders/" + CurrencyCode);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<Cardholder>();
    }
    
    public async Task<RefCurrencyCode> GetCurrency(int CurrencyCode)
    {
        var token = await GetAccessToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var response = await _httpClient.GetAsync($"{apiHost}Currencies/" + CurrencyCode);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<RefCurrencyCode>();
    }
    
    public async Task<RefCardType> GetCardType(int CurrencyCode)
    {
        var token = await GetAccessToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var response = await _httpClient.GetAsync($"{apiHost}CardTypes/" + CurrencyCode);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<RefCardType>();
    }

    public async Task<List<FinancialTransaction>> SearchTransaction(SearchViewModel model)
    {
        var token = await GetAccessToken();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var tokenRequest = new HttpRequestMessage(HttpMethod.Post, $"{apiHost}Search")
        {
            Content = new StringContent(JsonSerializer.Serialize(new
            {
                startDate = model.StartDate,
                endDate = model.EndDate,
                currencyCodes = model.CurrencyCodes,
                merchantCodePrefix = model.MerchantCodePrefix ?? ""
            }), Encoding.UTF8, "application/json")
        };
        
        var response = await _httpClient.SendAsync(tokenRequest);

        response.EnsureSuccessStatusCode();
        var res = await response.Content.ReadFromJsonAsync<Response<FinancialTransaction>>();
        return res.Result;
    }
    
    public async Task<List<RefCardType>> GetCardTypesAsync()
    {
        var response = await _httpClient.GetAsync($"{apiHost}Reference/CardTypes");
        response.EnsureSuccessStatusCode();
        var res = await response.Content.ReadFromJsonAsync<Response<RefCardType>>();
        return res.Result;
    }
    
    //idk, .net response moment
    public sealed record Response<T>(
        [property: JsonPropertyName("$values")] List<T> Result
    );
}
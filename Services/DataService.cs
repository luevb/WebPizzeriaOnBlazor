using System.Text.Json;
using System.Text;

namespace BlazorPizzeria.Services;

public class DadataService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _secretKey;

    public DadataService(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _apiKey = config["Dadata:ApiKey"] ?? throw new Exception("Missing Dadata:ApiKey");
        _secretKey = config["Dadata:SecretKey"] ?? throw new Exception("Missing Dadata:SecretKey");
    }

    public async Task<List<string>> SuggestAddressAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query) || query.Length < 3)
            return new List<string>();

        var request = new
        {
            query = query,
            count = 5,
            from_bound = new { value = "street" },
            to_bound = new { value = "house" }
        };

        var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Token {_apiKey}");
        _httpClient.DefaultRequestHeaders.Add("X-Secret", _secretKey);

        var response = await _httpClient.PostAsync("https://suggestions.dadata.ru/suggestions/api/4_1/rs/suggest/address", content);
        if (!response.IsSuccessStatusCode)
            return new List<string>();

        var json = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(json);
        var suggestions = doc.RootElement.GetProperty("suggestions");
        var result = new List<string>();
        foreach (var item in suggestions.EnumerateArray())
        {
            var value = item.GetProperty("value").GetString();
            if (!string.IsNullOrEmpty(value))
                result.Add(value);
        }
        return result;
    }
}
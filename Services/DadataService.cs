using Dadata;

namespace BlazorPizzeria.Services;

public class DadataService
{
    private readonly SuggestClientAsync _suggestClient;

    public DadataService(IConfiguration configuration)
    {
        var apiKey = configuration["Dadata:ApiKey"];
        var secretKey = configuration["Dadata:SecretKey"];
        _suggestClient = new SuggestClientAsync(apiKey, secretKey);
    }

    public async Task<List<string>> SuggestAddressAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query) || query.Length < 3)
            return new List<string>();

        try
        {
            var response = await _suggestClient.SuggestAddress(query);
            return response.suggestions
                .Select(s => s.value)
                .ToList();
        }
        catch (Exception ex)
        {
            // Вы можете залогировать ошибку
            Console.WriteLine($"Ошибка DaData: {ex.Message}");
            return new List<string>();
        }
    }
}
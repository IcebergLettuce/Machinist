

using System.Text.Json;

namespace ControlPlane;

public class YahooProvider : IMarketDataProvider
{
    public YahooProvider(IHttpClientFactory httpClientFactory)
        => _httpClientFactory = httpClientFactory;
    public async Task<bool> TickerIsValid(string ticker)
    {
        var httpClient = _httpClientFactory.CreateClient("YahooFinance");
        var httpResponseMessage = await httpClient.GetAsync($"?symbols={ticker}");
        httpResponseMessage.EnsureSuccessStatusCode();

        using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
        var response = await JsonSerializer.DeserializeAsync<TickerMetaData>(contentStream);

        if(response == null)
            return false;

        return response.quoteResponse.result.Count == 0 ? false : true;
    }

    public async Task<string?> TickerToName(string ticker)
    {
        var stock = await GetStockMetaData(ticker);
        return stock?.quoteResponse.result.First().displayName;
    }

    private async Task<TickerMetaData?> GetStockMetaData(string ticker)
    {
        var httpClient = _httpClientFactory.CreateClient("YahooFinance");
        var httpResponseMessage = await httpClient.GetAsync($"?symbols={ticker}");
        httpResponseMessage.EnsureSuccessStatusCode();
        using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<TickerMetaData>(contentStream);
    }

    private readonly IHttpClientFactory _httpClientFactory;

}
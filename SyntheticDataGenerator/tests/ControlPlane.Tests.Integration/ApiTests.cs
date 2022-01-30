using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace ControlPlane.Tests.Integration;

public class UnitTest1 : WebApplicationFactoryFixture
{

    [Fact]
    public async Task test_availability_of_swaggerPage()
    {

        var application = new WebApplicationFactoryFixture();
        var client = application.CreateClient();
        var response = await client.GetAsync("/swagger/index.html");
        Assert.True(response.IsSuccessStatusCode, "web api swagger page is accessible.");
    }

    [Fact]
    public async Task test_simple_get_request()
    {
        var application = new WebApplicationFactoryFixture();
        var client = application.CreateClient();
        var response = await client.GetAsync("/StockTicker");
        Assert.True(response.IsSuccessStatusCode);

    }

    [Theory]
    [InlineData("MSFT", "MSFT")]
    [InlineData("HOOD", "HOOD")]
    public async Task test_post_and_get(string value1, string value2)
    {

        var application = new WebApplicationFactoryFixture();
        var client = application.CreateClient();

        var c = JsonSerializer.Serialize(new StockDto { ticker = value1 });
        var payload = new StringContent(c, Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/StockTicker", payload);
        Assert.True(response.IsSuccessStatusCode);

        var check = await client.GetAsync("/StockTicker");
        var xx = await check.Content.ReadAsStreamAsync();
        var content = await JsonSerializer.DeserializeAsync<List<Stock>>(xx);

        Assert.NotNull(content);
        Assert.True(content.Select(x => x.ticker).ToList().Contains(value2));

    }
    // protected override void ConfigureWebHost(IWebHostBuilder builder)
    // {
    //     System.Console.WriteLine("HAllooooooo");

    //     builder.ConfigureServices(services =>
    //     {
    //         services.AddSingleton<IDatabaseProvider, DatabaseProviderMock>();
    //         services.AddSingleton<IMarketDataProvider, MarketDataProviderMock>();

    //     });
    // }
}
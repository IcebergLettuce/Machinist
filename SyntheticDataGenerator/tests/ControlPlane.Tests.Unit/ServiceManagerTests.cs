using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using ControlPlane.Tests.Shared;

namespace ControlPlane.Tests.Unit;

public class ServiceManagerTests
{
    private IStockManagerService _sut;
    public ServiceManagerTests()
    {
        var services = new ServiceCollection();
        services.AddTransient<IMarketDataProvider,MarketDataProviderMock>();
        services.AddTransient<IDatabaseProvider,DatabaseProviderMock>();
        services.AddTransient<IStockManagerService,StockManagerService>();    

        _sut = services.BuildServiceProvider().GetRequiredService<IStockManagerService>();
    }

    [Theory]
    [InlineData("MSFT")]
    [InlineData("HOOD")]
    [InlineData("ZOOM")]

    public async void add_get_stock_test(string value1)
    {
        var result = await _sut.AddStock(value1);
        Assert.NotNull(result);

        var stocks = await _sut.GetAllStocks();
        Assert.NotNull(stocks.Select(x=>x.ticker).ToList().Contains(value1));        
        Assert.True(stocks.Select(x=>x.ticker).ToList().Contains(value1));        
    }

    [Theory]
    [InlineData("MSFTXS")]
    [InlineData("")]
    [InlineData("Z")]    
    public async void add_invalid_stock_test(string value1)
    {
        var result = await _sut.AddStock(value1);
        Assert.Null(result);
    }

    [Fact]
    public async void add_same_ticker_multiple_times(){

        var stock = await _sut.AddStock("MSFT");
        await _sut.AddStock("MSFT");
        var stocks = await _sut.GetAllStocks();

        Assert.True(stocks.Count() == 1);

        var ff = await _sut.AddStock("MSFT");
        var stocks_2 = await _sut.GetAllStocks();

        Assert.True(stocks_2.Count() == 1);

    }
}
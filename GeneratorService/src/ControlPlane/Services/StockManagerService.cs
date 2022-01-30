using System.Text.Json;
using StackExchange.Redis;
using Confluent.Kafka; // TODO: Move
using System.Net;

namespace ControlPlane;
public class StockManagerService : IStockManagerService
{
    public StockManagerService(IDatabaseProvider databaseProvider, 
    IMarketDataProvider marketDataProvider)
    {
        _databaseProvider = databaseProvider;
        _marketDataProvider = marketDataProvider;
    }

    public async Task<Stock?> GetStock(string ticker)
    {
        return await _databaseProvider.GetStock(ticker);
    }

    public async Task<IEnumerable<Stock>?> GetAllStocks()
    {
        return await _databaseProvider.GetStocks();
    }

    public async Task<bool> DeleteStock(string ticker)
    {
        return await _databaseProvider.DeleteStock(ticker);
    }

    public async Task<Stock?> AddStock(string ticker)
    {
        if (await _marketDataProvider.TickerIsValid(ticker))
        {

            var name = await _marketDataProvider.TickerToName(ticker);
            if (name == null)
                return null;

            var stock = new Stock(ticker, name);
            return await _databaseProvider.AddStock(stock);
        }
        else
        {
            return null;
        }
    }

    private readonly IDatabaseProvider _databaseProvider;
    private readonly IMarketDataProvider _marketDataProvider;

}



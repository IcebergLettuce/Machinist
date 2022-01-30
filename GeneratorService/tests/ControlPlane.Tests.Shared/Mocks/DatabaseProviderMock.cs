
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlPlane.Tests.Shared;


public class DatabaseProviderMock : IDatabaseProvider
{
    private readonly List<Stock> _stocks = new List<Stock>();
    public async Task<Stock> AddStock(Stock stock)
    {
        if(_stocks.Exists(x => x.ticker == stock.ticker))
            return await Task.FromResult(stock);

        _stocks.Add(stock);
        return await Task.FromResult(stock);
    }

    public async Task<bool> DeleteStock(string ticker)
    {
        var count = _stocks.RemoveAll(x => x.ticker == ticker);
        if (count == 1)
            return await Task.FromResult(true);;
        
        return await Task.FromResult(false);;
    }

    public async Task<Stock?> GetStock(string ticker)
    {
        var stock = _stocks.Where(x=>x.ticker == ticker).FirstOrDefault();
        return await Task.FromResult(stock);
    }

    public async Task<IEnumerable<Stock>?> GetStocks()
    {
        return await Task.FromResult(_stocks);
    }
}


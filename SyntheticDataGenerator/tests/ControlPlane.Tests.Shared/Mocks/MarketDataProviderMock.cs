using System.Threading.Tasks;
using ControlPlane;

namespace ControlPlane.Tests.Shared;


public class MarketDataProviderMock : IMarketDataProvider
{
    public async Task<bool> TickerIsValid(string ticker)
    {
        if (ticker.Length > 5 || ticker.Length < 4)
            return await Task.FromResult(false);

        return await Task.FromResult(true);
    }

    public async Task<string?> TickerToName(string ticker)
    {
        return await Task.FromResult("SomeName");
    }
}

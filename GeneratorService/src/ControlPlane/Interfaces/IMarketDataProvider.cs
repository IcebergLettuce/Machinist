

namespace ControlPlane;


public interface IMarketDataProvider
{
    Task<bool> TickerIsValid(string ticker);
    Task<string?> TickerToName(string ticker);
}
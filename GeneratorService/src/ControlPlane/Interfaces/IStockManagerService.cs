
namespace ControlPlane
{
    public interface IStockManagerService {

        Task<Stock?> GetStock(string ticker);
        Task<IEnumerable<Stock>?> GetAllStocks();
        Task<bool> DeleteStock(string ticker);
        Task<Stock?> AddStock(string ticker);

    }
}
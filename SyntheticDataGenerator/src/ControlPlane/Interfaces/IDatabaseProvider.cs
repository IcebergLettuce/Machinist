
namespace ControlPlane {
    public interface IDatabaseProvider {

        Task<IEnumerable<Stock>?> GetStocks();
        Task<Stock?> GetStock(string ticker);
        Task<Stock> AddStock(Stock stock);
        Task<bool> DeleteStock(string ticker);

    }
}
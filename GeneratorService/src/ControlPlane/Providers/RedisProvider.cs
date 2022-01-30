using System.Text.Json;
using StackExchange.Redis;

namespace ControlPlane
{
    public class RedisProvider : IDatabaseProvider
    {
        public RedisProvider(IConnectionMultiplexer redis) => _redis = redis;

        public async Task<Stock> AddStock(Stock stock)
        {
            var stocks = await GetStocksFromRedis();
            var match = stocks.Where(x => x.ticker == stock.ticker)
                .FirstOrDefault();
            
            if(match != null)
                return stock;

            stocks.Add(stock);
            await WriteStocksToRedis(stocks);

            return stock;
        }
        public async Task<bool> DeleteStock(string ticker)
        {
            var stocks = await GetStocksFromRedis();
            var count = stocks.RemoveAll(x => x.ticker == ticker);
            await WriteStocksToRedis(stocks);

            if (count >= 1)
                return true;

            return false;
        }

        private async Task WriteStocksToRedis(List<Stock> stocks)
        {

            var db = _redis.GetDatabase();
            await db.StringSetAsync("Stocks", JsonSerializer.Serialize(stocks));
        }

        private async Task<List<Stock>> GetStocksFromRedis()
        {
            var db = _redis.GetDatabase();
            var contentJsonString = await db.StringGetAsync("Stocks");

            List<Stock>? stocks;

            if (contentJsonString == RedisValue.Null)
            {
                stocks = new List<Stock>();
            }
            else
            {
                stocks = JsonSerializer.Deserialize<List<Stock>>(contentJsonString);
                if (stocks == null)
                    stocks = new List<Stock>();
            }

            return stocks;
        }

        public async Task<Stock?> GetStock(string ticker)
        {
            var stocks = await GetStocksFromRedis();
            return stocks.Where(x=>x.ticker == ticker).FirstOrDefault();

        }

        public async Task<IEnumerable<Stock>?> GetStocks()
        {
            return await GetStocksFromRedis();
        }

        private readonly IConnectionMultiplexer _redis;

    }

}

namespace ControlPlane
{
    public class Stock
    {
        public string ticker { get; set; }
        public string tickerName { get; set; }
        public DateTimeOffset creationDateTime { get; set; }

        public Stock(string Ticker, string TickerName)
        {
            creationDateTime = new DateTimeOffset().UtcDateTime;
            this.ticker = Ticker;
            this.tickerName = TickerName;
        }

    }
}
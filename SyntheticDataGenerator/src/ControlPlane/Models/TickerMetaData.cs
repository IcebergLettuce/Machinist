namespace ControlPlane;
public class TickerMetaData
{
    public QuoteResponse quoteResponse { get; set; }
    public TickerMetaData() => quoteResponse = new QuoteResponse();
}

public class QuoteResponse
{
    public List<Result> result { get; set; }
    public QuoteResponse() => result = new List<Result>();
}
public class Result
{
    public string? displayName { get; set; }
}
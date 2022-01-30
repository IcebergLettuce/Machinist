using ControlPlane;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IStockManagerService, StockManagerService>();
builder.Services.AddTransient<IMarketDataProvider, YahooProvider>();
builder.Services.AddTransient<IDatabaseProvider, RedisProvider>();

builder.Services.AddSignalR();

builder.Services.AddHttpClient("YahooFinance", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://yfapi.net/v6/finance/quote");
    httpClient.DefaultRequestHeaders.Add("x-api-key","r0CXf1HA3f6Hqd5Xt8FYf8kqPJC8yzq56kxBgK4L");
});


var multiplexer = ConnectionMultiplexer.Connect("localhost");
builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();
app.MapHub<SimpleHub>("/notificationHub");

app.Run();
public partial class Program { }
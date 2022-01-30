
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Testing;
using ControlPlane.Tests.Shared;

namespace ControlPlane.Tests.Integration;

public class WebApplicationFactoryFixture : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {

        builder.ConfigureServices(services =>
        {
            services.AddSingleton<IDatabaseProvider, DatabaseProviderMock>();
            services.AddSingleton<IMarketDataProvider, MarketDataProviderMock>();

        });
    }
}

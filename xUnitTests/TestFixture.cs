using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace xUnitTests
{
    public class TestFixture : IDisposable
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public TestFixture()
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
            {
                // Register services here
                services.AddSingleton<IMyService, MyService>();
            })
            .Build();

            ServiceProvider = host.Services;
        }

        public void Dispose()
        {
            // Clean up resources if needed
        }
    }
}
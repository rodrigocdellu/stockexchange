using Microsoft.Extensions.DependencyInjection;
using StockExchange.WebAPI.Helpers;
using StockExchange.WebAPI.Services;

namespace StockExchange.WebAPI.Test.UnitTests.Base
{
    public class UnitTestBase
    {
        protected const string TEST_ANOTHEREXCEPTION_MESSAGE = "Another exception was thrown";
        protected ServiceProvider _Provider;

        [OneTimeSetUp]
        protected void Start()
        {
            // Create a collection of services for dependency injection
            var services = new ServiceCollection();

            // Records real dependencies
            services.AddTransient<IApplicationService, ApplicationService>();
            services.AddTransient<ICdbService, CdbService>();
            services.AddTransient<ITimeZoneProvider, SystemTimeZoneProvider>();

            // Builds the provider (container)
            this._Provider = services.BuildServiceProvider();
        }

        [OneTimeTearDown]
        protected void End()
        {
            // Dispose the provider (container)
            this._Provider.Dispose();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using StockExchange.WebAPI.Helpers;
using StockExchange.WebAPI.Services;
using StockExchange.WebAPI.Test.UnitTests.Base;

namespace StockExchange.WebAPI.Test.UnitTests;

[TestFixture]
public sealed class UT_CdbController : UnitTestBase
{
    [Test]
    public void Test_ApplicationTimeZone()
    {
        try
        {
            // Arrange
            var timeZone = DateTime.UtcNow.PrepareForDockerization().DisplayName;

            // Get the service via dependency injection
            var service = this._Provider.GetRequiredService<IApplicationService>();

            // Act
            var result = service.TimeZone;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.Not.Empty);
                Assert.That(result, Is.TypeOf<string>());
                Assert.That(result, Is.EqualTo(timeZone));
            });
        }
        catch (Exception exception)
        {
            // Fail on exception
            Assert.Fail($"{UT_ApplicationService.TEST_ANOTHEREXCEPTION_MESSAGE}: {exception.Message}");
        }
    }
}

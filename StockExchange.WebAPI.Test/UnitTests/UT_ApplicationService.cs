using Microsoft.Extensions.DependencyInjection;
using StockExchange.WebAPI.Helpers;
using StockExchange.WebAPI.Services;
using StockExchange.WebAPI.Test.Helpers;
using StockExchange.WebAPI.Test.UnitTests.Base;
using System.Runtime.InteropServices;

namespace StockExchange.WebAPI.Test.UnitTests;

[TestFixture]
public sealed class UT_ApplicationService : UnitTestBase
{
    [Test]
    public void Test_TryGetTimeZone()
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
            Assert.Fail($"{GeneralHelper.ANOTHEREXCEPTION_MESSAGE}: {exception.Message}");
        }
    }

    [Test]
    public void Test_TryGetStartupTime()
    {
        try
        {
            // Arrange
            var utcNow = DateTime.UtcNow;
            var startupTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, utcNow.PrepareForDockerization());

            // Get the service via dependency injection
            var service = this._Provider.GetRequiredService<IApplicationService>();

            // Act
            var result = service.StartupTime;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.ToString(), Is.Not.Empty);
                Assert.That(result, Is.TypeOf<DateTime>());
                Assert.That(result, Is.GreaterThanOrEqualTo(startupTime));
            });
        }
        catch (Exception exception)
        {
            // Fail on exception
            Assert.Fail($"{GeneralHelper.ANOTHEREXCEPTION_MESSAGE}: {exception.Message}");
        }        
    }

    [Test]
    public void Test_TryGetFrameworkVersion()
    {
        try
        {
            // Arrange
            var frameworkVersion = RuntimeInformation.FrameworkDescription;

            // Get the service via dependency injection
            var service = this._Provider.GetRequiredService<IApplicationService>();

            // Act
            var result = service.FrameworkVersion;

            // Assert
            Assert.Multiple(() =>
            {

                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.Not.Empty);
                Assert.That(result, Is.TypeOf<string>());
                Assert.That(result, Is.EqualTo(frameworkVersion));
            });
        }
        catch (Exception exception)
        {
            // Fail on exception
            Assert.Fail($"{GeneralHelper.ANOTHEREXCEPTION_MESSAGE}: {exception.Message}");
        }
    }    
}

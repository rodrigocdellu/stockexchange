using Microsoft.Extensions.DependencyInjection;
using StockExchange.WebAPI.Helpers;
using StockExchange.WebAPI.Services;
using StockExchange.WebAPI.Test.UnitTests.Base;
using System.Runtime.InteropServices;

namespace StockExchange.WebAPI.Test.UnitTests;

public sealed class UT_ApplicationService : UnitTestBase
{
    [Test]
    public void Test_ApplicationTimeZone()
    {
        try
        {
            // Arrange
            var brasilianTimeZone = DateTime.UtcNow.PrepareForDockerization().DisplayName;

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
                Assert.That(result, Is.EqualTo(brasilianTimeZone));
            });
        }
        catch (Exception exception)
        {
            // Fail on exception
            Assert.Fail($"{UT_ApplicationService.TEST_ANOTHEREXCEPTION_MESSAGE}: {exception.Message}");
        }
    }

    [Test]
    public void Test_ApplicationStartupTime()
    {
        try
        {
            // Arrange
            var utcNow = DateTime.UtcNow;
            var brasilianStartupTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, utcNow.PrepareForDockerization());

            // Get the service via dependency injection
            var service = this._Provider.GetRequiredService<IApplicationService>();

            // Act
            var result = service.StartupTime;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.ToString(), Is.Not.Empty);
                Assert.That(result, Is.TypeOf<DateTime>());
                Assert.That(result, Is.GreaterThanOrEqualTo(brasilianStartupTime));
            });
        }
        catch (Exception exception)
        {
            // Fail on exception
            Assert.Fail($"{UT_ApplicationService.TEST_ANOTHEREXCEPTION_MESSAGE}: {exception.Message}");
        }        
    }

    [Test]
    public void Test_ApplicationFrameworkVersion()
    {
        try
        {
            // Arrange
            var frameworkVersiona = RuntimeInformation.FrameworkDescription;

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
                Assert.That(result, Is.EqualTo(frameworkVersiona));
            });
        }
        catch (Exception exception)
        {
            // Fail on exception
            Assert.Fail($"{UT_ApplicationService.TEST_ANOTHEREXCEPTION_MESSAGE}: {exception.Message}");
        }
    }    
}

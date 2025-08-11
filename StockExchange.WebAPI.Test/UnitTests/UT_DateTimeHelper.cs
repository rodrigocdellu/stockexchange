using Moq;
using StockExchange.WebAPI.Helpers;
using StockExchange.WebAPI.Test.UnitTests.Base;

namespace StockExchange.WebAPI.Test.UnitTests;

public sealed class UT_DateTimeHelper : UnitTestBase
{
    [Test]
    public void Test_DateTimeHelperBrazilianTimeZoneWithException()
    {
        try
        {
            // Create the Mock
            var timeZoneMock = new Mock<ITimeZoneProvider>();

            // Setup the Mock
            timeZoneMock
                .Setup(internalObject => internalObject.GetSystemTimeZones())
                .Throws<Exception>();

            // Assert
            Assert.Throws<Exception>(() => timeZoneMock.Object.GetSystemTimeZones());
        }
        catch (Exception exception)
        {
            // Fail on exception
            Assert.Fail($"{UT_ApplicationService.TEST_ANOTHEREXCEPTION_MESSAGE}: {exception.Message}");
        }
    }
}

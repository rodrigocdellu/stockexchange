using Moq;
using StockExchange.WebAPI.Helpers;
using StockExchange.WebAPI.Test.UnitTests.Base;
using System.Reflection;

namespace StockExchange.WebAPI.Test.UnitTests;

public sealed class UT_DateTimeHelper : UnitTestBase
{
    [Test]
    public void Test_DateTimeHelperBrazilianTimeZoneWithException()
    {
        try
        {
            // Arrange
            var method = typeof(DateTimeHelper).GetMethod("GetBrasilianTimeZone", BindingFlags.NonPublic | BindingFlags.Static);

            // Create the Mock
            var timeZoneMock = new Mock<ITimeZoneProvider>();

            // Setup the Mock
            timeZoneMock
                .Setup(internalObject => internalObject.GetSystemTimeZones())
                .Throws<Exception>();

            // Act
            var result = (TimeZoneInfo?)method!.Invoke(null, new object[] { timeZoneMock.Object });

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<TimeZoneInfo?>());
                Assert.That(result!.Id, Is.EqualTo("UTC-3"));
                Assert.That(result!.BaseUtcOffset, Is.EqualTo(TimeSpan.FromHours(-3)));
                Assert.That(result!.DisplayName, Is.EqualTo("(UTC-3) Horário de São Paulo"));
                Assert.That(result!.StandardName, Is.EqualTo("UTC-3"));
            });
        }
        catch (Exception exception)
        {
            // Fail on exception
            Assert.Fail($"{UT_DateTimeHelper.TEST_ANOTHEREXCEPTION_MESSAGE}: {exception.Message}");
        }
    }
}

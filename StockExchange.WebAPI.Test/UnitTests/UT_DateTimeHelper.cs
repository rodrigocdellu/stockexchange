using Moq;
using StockExchange.WebAPI.Helpers;

namespace StockExchange.WebAPI.Test.UnitTests;

public class UT_DateTimeHelper
{
    [Test]
    public void Test_PreparacaoDockerization()
    {
        // Act
        var targetTimeZone = DateTime.UtcNow.PrepareForDockerization();
        var brasilianTimeZone = DateTimeHelper.GetBrasilianTimeZone(new SystemTimeZoneProvider());

        // Do the tests
        Assert.Multiple(() =>
        {
            Assert.That(targetTimeZone, Is.Not.Null);
            Assert.That(targetTimeZone!.Id, Is.EqualTo(brasilianTimeZone!.Id));
            Assert.That(targetTimeZone!.BaseUtcOffset, Is.EqualTo(brasilianTimeZone!.BaseUtcOffset));
        });
    }

    [Test]
    public void Test_FusoHorarioBrazileiro()
    {
        // Act
        var result = DateTimeHelper.GetBrasilianTimeZone(new SystemTimeZoneProvider());

        // Do the tests
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Id, Is.AnyOf("UTC-3", "E. South America Standard Time"));
            Assert.That(result!.BaseUtcOffset, Is.EqualTo(TimeSpan.FromHours(-3)));
        });
    }

    [Test]
    public void Test_FusoHorarioBrazileiroExcecao()
    {
        // Create the Mock
        var mockValidator = new Mock<ITimeZoneProvider>();

        // Setup the Mock
        mockValidator
            .Setup(v => v.GetSystemTimeZones())
            .Throws<Exception>();

        // Act
        var result = DateTimeHelper.GetBrasilianTimeZone(mockValidator.Object);

        // Do the tests
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Id, Is.EqualTo("UTC-3"));
            Assert.That(result!.BaseUtcOffset, Is.EqualTo(TimeSpan.FromHours(-3)));
        });
    }
}

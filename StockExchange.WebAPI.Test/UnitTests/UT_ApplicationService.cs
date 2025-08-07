using StockExchange.WebAPI.Helpers;
using StockExchange.WebAPI.Services;

namespace StockExchange.WebAPI.Test.UnitTests;

public class UT_ApplicationService
{
    private IApplicationService? _ApplicationService;

    [SetUp]
    public void Setup()
    {
        // Create the service
        this._ApplicationService = new ApplicationService();
    }

    [Test]
    public void Test_FusoHorario()
    {
        // Load data
        var brasilianTimeZone = DateTimeHelper.GetBrasilianTimeZone(new SystemTimeZoneProvider());

        // Do the initial tests
        Assert.Multiple(() =>
        {
            Assert.That(brasilianTimeZone, Is.Not.Null);
            Assert.That(this._ApplicationService, Is.Not.Null);
        });
        
        // Act
        var result = this._ApplicationService.TimeZone;

        // Do the tests
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.EqualTo(brasilianTimeZone.DisplayName));
        });
    }

    [Test]
    public void Test_TempoInicializacao()
    {
        // Do the initial test
        Assert.That(this._ApplicationService, Is.Not.Null);

        // Act
        var result = this._ApplicationService.StartupTime;

        // Do the tests
        Assert.Multiple(() =>
        {
            Assert.That(result.ToString(), Is.Not.Empty);
            Assert.That(result.GetType(), Is.EqualTo(typeof(DateTime)));
            Assert.That(result, Is.LessThanOrEqualTo(DateTime.Now));
        });
    }

    [Test]
    public void Test_VersaoFramework()
    {
        // Do the initial test
        Assert.That(this._ApplicationService, Is.Not.Null);

        // Act
        var result = this._ApplicationService.FrameworkVersion;

        // Do the tests
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
        });
    }    
}

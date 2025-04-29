using StockExchange.WebAPI.Services;
using StockExchange.WebAPI.Test.Helpers;

namespace StockExchange.WebAPI.Test.Services;

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
        var brasilianTimeZone = TestHelper.GetBrasilianTimeZone();

        // Do the initial tests
        Assert.Multiple(() =>
        {
            Assert.That(brasilianTimeZone, Is.Not.Null);
            Assert.That(this._ApplicationService, Is.Not.Null);
        });
        
        // Call the service
        var retorno = this._ApplicationService.TimeZone;

        // Do the tests
        Assert.Multiple(() =>
        {
            Assert.That(retorno, Is.Not.Null);
            Assert.That(retorno, Is.Not.Empty);
            Assert.That(retorno, Is.EqualTo(brasilianTimeZone.DisplayName));
        });
    }

    [Test]
    public void Test_TempoInicializacao()
    {
        // Do the initial test
        Assert.That(this._ApplicationService, Is.Not.Null);

        // Call the service
        var retorno = this._ApplicationService.StartupTime;

        // Do the tests
        Assert.Multiple(() =>
        {
            Assert.That(retorno.ToString(), Is.Not.Empty);
            Assert.That(retorno.GetType(), Is.EqualTo(typeof(DateTime)));
            Assert.That(retorno, Is.LessThanOrEqualTo(DateTime.Now));
        });
    }

    [Test]
    public void Test_VersaoFramework()
    {
        // Do the initial test
        Assert.That(this._ApplicationService, Is.Not.Null);

        // Call the service
        var retorno = this._ApplicationService.FrameworkVersion;

        // Do the tests
        Assert.Multiple(() =>
        {
            Assert.That(retorno, Is.Not.Null);
            Assert.That(retorno, Is.Not.Empty);
        });
    }    
}

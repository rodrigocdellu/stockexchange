using StockExchange.WebAPI.Services;
using StockExchange.WebAPI.Test.Helpers;

namespace StockExchange.WebAPI.Test.Services;

public class UT_CdbService
{
    private ICdbService? _CdbService;

    private List<RetornoContainerHelper>? Samples { get; set; }

    [SetUp]
    public void Setup()
    {
        // Load data
        this.Samples = TestHelper.LoadData();

        // Create the service
        this._CdbService = new CdbService();
    }

    [Test]
    public void Test_ResultadosInvestimentosValidos()
    {            
        // Do the initial tests
        Assert.Multiple(() =>
        {
            Assert.That(this.Samples, Is.Not.Null);
            Assert.That(this._CdbService, Is.Not.Null);
        });

        // Scan the list
        foreach (var sample in this.Samples)
        {
            // Scan the list
            foreach (var retornoValido in sample.RetornosValidos)
            {
                // Define the variables
                decimal investimento;
                uint meses;
                decimal resultadoBruto;
                decimal resultadoLiquido;

                // Cast variables for the service
                TestHelper.CastData(retornoValido, out investimento, out meses, out resultadoBruto, out resultadoLiquido);

                // Call the service
                var retorno = this._CdbService.SolicitarCalculoInvestimento(investimento, meses).Result.Data;

                // If there is no data, the test fail
                if (retorno == null)
                    Assert.Fail();
                else
                    // Do the tests
                    Assert.Multiple(() =>
                    {
                        Assert.That(retorno.ResultadoBruto, Is.EqualTo(resultadoBruto));
                        Assert.That(retorno.ResultadoLiquido, Is.EqualTo(resultadoLiquido));
                    });
            }
        }
    }

    [Test]
    public void Test_ResultadosInvestimentosInvalidos()
    {
        // Do the initial tests
        Assert.Multiple(() =>
        {
            Assert.That(this.Samples, Is.Not.Null);
            Assert.That(this._CdbService, Is.Not.Null);
        });

        // Scan the list
        foreach (var sample in this.Samples)
        {
            // Scan the list
            foreach (var retornoInvalido in sample.RetornosInvalidos)
            {
                // Define the variables
                decimal investimento;
                uint meses;
                decimal resultadoBruto;
                decimal resultadoLiquido;

                // Cast variables for the service
                TestHelper.CastData(retornoInvalido, out investimento, out meses, out resultadoBruto, out resultadoLiquido);

                // Call the service
                var retorno = this._CdbService.SolicitarCalculoInvestimento(investimento, meses).Result.Data;

                // If there is no data, the test fail
                if (retorno == null)
                    Assert.Fail();
                else
                    // Do the tests
                    Assert.Multiple(() =>
                    {
                        Assert.That(retorno.ResultadoBruto, !Is.EqualTo(resultadoBruto));
                        Assert.That(retorno.ResultadoLiquido, !Is.EqualTo(resultadoLiquido));
                    });
            }
        }
    }

    [Test]
    public void Test_MesesZero()
    {
        // Load data
        decimal investimento = 1m;
        uint meses = 0U;

        // Do the initial test
        Assert.That(this._CdbService, Is.Not.Null);

        // Call the service
        var retorno = this._CdbService.SolicitarCalculoInvestimento(investimento, meses).Result.Data;

        // Do the tests
        Assert.That(retorno, Is.Null);
    }

    [Test]
    public void Test_InvestimentoNegativo()
    {
        // Load data
        decimal investimento = -1m;
        uint meses = 1U;

        // Do the initial test
        Assert.That(this._CdbService, Is.Not.Null);

        // Call the service
        var retorno = this._CdbService.SolicitarCalculoInvestimento(investimento, meses).Result.Data;

        // Do the tests
        Assert.That(retorno, Is.Null);
    }
}

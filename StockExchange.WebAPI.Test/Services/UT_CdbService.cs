using StockExchange.WebAPI.DTOs;
using StockExchange.WebAPI.Services;
using StockExchange.WebAPI.Test.Helpers;
using StockExchange.WebAPI.Validators;

namespace StockExchange.WebAPI.Test.Services;

public class UT_CdbService
{
    private const string? TEST_MESESZERO_MESSAGE = "O parâmetro 'meses' deve ser maior que 0 e menor do que 12001. Valor fornecido: '0'.\r\n";
    private const string? TEST_INVESTIMENTONEGATIVO_MESSAGE = "O parâmetro 'valor' deve ser maior que 0.00. Valor fornecido: '-1'.\r\n";

    private ICdbService? _CdbService;

    private List<RetornoContainerHelper>? Samples { get; set; }

    [SetUp]
    public void Setup()
    {
        // Create the service
        this._CdbService = new CdbService(new InvestimentoValidator());

        // Load data
        this.Samples = TestHelper.LoadData();        
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
                var retorno = this._CdbService.SolicitarCalculoInvestimento(new InvestimentoDto() { Valor = investimento, Meses = meses }).Result.Data;

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
                var retorno = this._CdbService.SolicitarCalculoInvestimento(new InvestimentoDto() { Valor = investimento, Meses = meses }).Result.Data;

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
        var investimento = new InvestimentoDto() { Valor = 1m, Meses = 0U };

        // Do the initial test
        Assert.That(this._CdbService, Is.Not.Null);
        
        // Call the service
        var retorno = this._CdbService.SolicitarCalculoInvestimento(investimento).Result;

        // Do the tests
        Assert.Multiple(() =>
        {
            Assert.That(retorno.Data, Is.Null);
            Assert.That(retorno.ErrorMessage, Is.EqualTo(UT_CdbService.TEST_MESESZERO_MESSAGE));
            Assert.That(retorno.Success, Is.False);
        });
    }

    [Test]
    public void Test_InvestimentoNegativo()
    {
        // Load data
        var investimento = new InvestimentoDto() { Valor = -1m, Meses = 1U };

        // Do the initial test
        Assert.That(this._CdbService, Is.Not.Null);

        // Call the service
        var retorno = this._CdbService.SolicitarCalculoInvestimento(investimento).Result;

        // Do the tests
        Assert.Multiple(() =>
        {
            Assert.That(retorno.Data, Is.Null);
            Assert.That(retorno.ErrorMessage, Is.EqualTo(UT_CdbService.TEST_INVESTIMENTONEGATIVO_MESSAGE));
            Assert.That(retorno.Success, Is.False);
        });
    }
}

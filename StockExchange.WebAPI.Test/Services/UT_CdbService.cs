using FluentValidation;
using Moq;
using StockExchange.WebAPI.DTOs;
using StockExchange.WebAPI.Services;
using StockExchange.WebAPI.Test.Helpers;

namespace StockExchange.WebAPI.Test.Services;

public class UT_CdbService
{
    private const string TEST_MESESMINIMO_MESSAGE = "O parâmetro 'meses' deve ser maior que 1 e menor do que 1201. Valor fornecido: '1'.";

    private const string TEST_MESESMAXIMO_MESSAGE = "O parâmetro 'meses' deve ser maior que 1 e menor do que 1201. Valor fornecido: '1201'.";

    private const string TEST_INVESTIMENTONEGATIVO_MESSAGE = "O parâmetro 'valor' deve ser maior que 0.00. Valor fornecido: '-1'.";

    //private const string TEST_INVESTIMENTOZERO_MESSAGE = "O parâmetro 'valor' deve ser maior que 0.00. Valor fornecido: '0'.";

    private const string TEST_INVESTIMENTOEXCECAO_MESSAGE = "Exceção forçada para testes.";

    private List<RetornoContainerHelper>? Samples { get; set; }

    [SetUp]
    public void Setup()
    {        
        // Load data
        this.Samples = TestHelper.LoadData();        
    }

    [Test]
    public void Test_ResultadosInvestimentosValidos()
    {
        // Do the initial tests
        Assert.That(this.Samples, Is.Not.Null);

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

                // Create the service
                var service = new CdbService();

                // Call the service
                var retorno = service.SolicitarCalculoInvestimento(new InvestimentoDto() { Valor = investimento, Meses = meses }).Result.Data;

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
        Assert.That(this.Samples, Is.Not.Null);

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

                // Create the service
                var service = new CdbService();

                // Call the service
                var retorno = service.SolicitarCalculoInvestimento(new InvestimentoDto() { Valor = investimento, Meses = meses }).Result.Data;

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

    /// <summary>
    /// O valor mínimo de meses deve ser maior que 1.
    /// </summary>
    [Test]
    public void Test_MesesMinimo()
    {
        // Load data
        var investimento = new InvestimentoDto() { Valor = 1m, Meses = 1u };

        // Create the service
        var service = new CdbService();
        
        // Call the service
        var retorno = service.SolicitarCalculoInvestimento(investimento).Result;

        // Do the tests
        Assert.Multiple(() =>
        {
            Assert.That(retorno.Data, Is.Null);
            Assert.That(retorno.ErrorMessage, Is.EqualTo(UT_CdbService.TEST_MESESMINIMO_MESSAGE.Concat(Environment.NewLine)));
            Assert.That(retorno.Success, Is.False);
        });
    }

    /// <summary>
    /// Adicionado por conta de uma solicitação de segurança do SonarQube.
    /// </summary>
    [Test]
    public void Test_MesesMaximo()
    {
        // Load data
        var investimento = new InvestimentoDto() { Valor = 1m, Meses = 1201u };

        // Create the service
        var service = new CdbService();

        // Call the service
        var retorno = service.SolicitarCalculoInvestimento(investimento).Result;

        // Do the tests
        Assert.Multiple(() =>
        {
            Assert.That(retorno.Data, Is.Null);
            Assert.That(retorno.ErrorMessage, Is.EqualTo(UT_CdbService.TEST_MESESMAXIMO_MESSAGE.Concat(Environment.NewLine)));
            Assert.That(retorno.Success, Is.False);
        });
    }

    [Test]
    public void Test_InvestimentoNegativo()
    {
        // Load data
        var investimento = new InvestimentoDto() { Valor = -1m, Meses = 2u };

        // Create the service
        var service = new CdbService();

        // Call the service
        var retorno = service.SolicitarCalculoInvestimento(investimento).Result;

        // Do the tests
        Assert.Multiple(() =>
        {
            Assert.That(retorno.Data, Is.Null);
            Assert.That(retorno.ErrorMessage, Is.EqualTo(UT_CdbService.TEST_INVESTIMENTONEGATIVO_MESSAGE.Concat(Environment.NewLine)));
            Assert.That(retorno.Success, Is.False);
        });
    }

    /*
    [Test]
    public void Test_InvestimentoZero()
    {
        // Load data
        var investimento = new InvestimentoDto() { Valor = 0m, Meses = 2u };

        // Create the service
        var service = new CdbService();

        // Call the service
        var retorno = service.SolicitarCalculoInvestimento(investimento).Result;

        // Do the tests
        Assert.Multiple(() =>
        {
            Assert.That(retorno.Data, Is.Null);
            Assert.That(retorno.ErrorMessage, Is.EqualTo(UT_CdbService.TEST_INVESTIMENTOZERO_MESSAGE.Concat(Environment.NewLine)));
            Assert.That(retorno.Success, Is.False);
        });
    }
    */

    [Test]
    public void Test_InvestimentoExcecao()
    {
        // Load data
        var investimento = new InvestimentoDto() { Valor = 0m, Meses = 0u };

        // Create the Mock
        var mockValidator = new Mock<IValidator<InvestimentoDto>>();

        // Setup the Mock
        mockValidator
            .Setup(v => v.Validate(It.IsAny<InvestimentoDto>()))
            .Throws(new Exception(UT_CdbService.TEST_INVESTIMENTOEXCECAO_MESSAGE));

        // Create the service
        var service = new CdbService(mockValidator.Object);

        // Call the service
        var retorno = service.SolicitarCalculoInvestimento(investimento).Result;

        // Do the tests
        Assert.Multiple(() =>
        {
            Assert.That(retorno.Data, Is.Null);
            Assert.That(retorno.ErrorMessage, Is.EqualTo(UT_CdbService.TEST_INVESTIMENTOEXCECAO_MESSAGE));
            Assert.That(retorno.Success, Is.False);
        });
    }
}

using Microsoft.AspNetCore.Mvc;
using Moq;
using StockExchange.WebAPI.Controllers;
using StockExchange.WebAPI.DTOs;
using StockExchange.WebAPI.Helpers;
using StockExchange.WebAPI.Services;
using StockExchange.WebAPI.Test.Helpers;
using StockExchange.WebAPI.Test.UnitTests.Base;
using System.Net;

namespace StockExchange.WebAPI.Test.UnitTests;

[TestFixture]
public sealed class UT_CdbController : UnitTestBase
{
    private Mock<ICdbService> _CDBServiceMock;
    private CdbController _CDBController;

    [SetUp]
    public void SetUp()
    {
        this._CDBServiceMock = new Mock<ICdbService>();
        this._CDBController = new CdbController(this._CDBServiceMock.Object);
    }

    [Test]
    public async Task Test_TryGetValidInvestmentResults_ShouldReturnOk()
    {
        try
        {
            // Arrange
            var investimento = new InvestimentoDto() { Valor = 1m, Meses = 2u };
            var retornoEsperado = new RetornoDto() { ResultadoBruto = 1.0195344784m, ResultadoLiquido = 1.0151392207600m };

            this._CDBServiceMock
                .Setup(internalObject => internalObject.SolicitarCalculoInvestimento(investimento))
                .ReturnsAsync(new ServiceResultHelper<RetornoDto>
                {
                    Success = true,
                    Data = retornoEsperado
                });

            // Act
            var retorno = await this._CDBController.SolicitarCalculoInvestimento(investimento);
            var retornoOk = retorno as OkObjectResult;
            var retornoDto = retornoOk?.Value as RetornoDto;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(retornoOk, Is.Not.Null);
                Assert.That(retornoOk!.Value, Is.InstanceOf<RetornoDto>());
                Assert.That(retornoDto!.ResultadoBruto, Is.EqualTo(retornoEsperado.ResultadoBruto));
                Assert.That(retornoDto!.ResultadoLiquido, Is.EqualTo(retornoEsperado.ResultadoLiquido));
            });
        }
        catch (Exception exception)
        {
            // Fail on exception
            Assert.Fail($"{GeneralHelper.ANOTHEREXCEPTION_MESSAGE}: {exception.Message}");
        }
    }

    [Test]
    public async Task Test_TryGetValidInvestmentResults_ShouldReturnBadRequest()
    {
        try
        {
            // Arrange
            var investimento = new InvestimentoDto() { Valor = 1m, Meses = 2u };

            this._CDBServiceMock
                .Setup(internalObject => internalObject.SolicitarCalculoInvestimento(investimento))
                .ReturnsAsync(new ServiceResultHelper<RetornoDto>
                {
                    Success = false,
                    ErrorMessage = GeneralHelper.UNEXPECTEDERROR_MESSAGE,
                });

            // Act
            var retorno = await this._CDBController.SolicitarCalculoInvestimento(investimento);
            var retornoBad = retorno as BadRequestObjectResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(retornoBad, Is.Not.Null);
                Assert.That(retornoBad!.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
            });
        }
        catch (Exception exception)
        {
            // Fail on exception
            Assert.Fail($"{GeneralHelper.ANOTHEREXCEPTION_MESSAGE}: {exception.Message}");
        }
    }

    [Test]
    public async Task Test_TryGetValidInvestmentResults_ShouldReturnInternalServerError()
    {
        try
        {
            // Arrange
            var investimento = new InvestimentoDto() { Valor = 1m, Meses = 2u };

            this._CDBServiceMock
                .Setup(internalObject => internalObject.SolicitarCalculoInvestimento(investimento))
                .ThrowsAsync(new InvalidOperationException(GeneralHelper.UNEXPECTEDERROR_MESSAGE));

            // Act
            var retorno = await _CDBController.SolicitarCalculoInvestimento(investimento);
            var retornoResult = retorno as ObjectResult;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(retornoResult, Is.Not.Null);
                Assert.That(retornoResult!.StatusCode, Is.EqualTo((int)HttpStatusCode.InternalServerError));
            });
        }
        catch (Exception exception)
        {
            // Fail on exception
            Assert.Fail($"{GeneralHelper.ANOTHEREXCEPTION_MESSAGE}: {exception.Message}");
        }
    }

    [Test]
    public async Task Test_TryGetValidInvestmentResults_MonthsOutOfRange_ShouldReturnBadRequest()
    {
        try
        {
            // Arrange
            var investimento = new InvestimentoDto() { Valor = 1m, Meses = 1u };

            // Act
            var retorno = await this._CDBController.SolicitarCalculoInvestimento(investimento);

            // Assert
            Assert.That(retorno, Is.TypeOf<BadRequestObjectResult>());
        }
        catch (Exception exception)
        {
            // Fail on exception
            Assert.Fail($"{GeneralHelper.ANOTHEREXCEPTION_MESSAGE}: {exception.Message}");
        }
    }

    [Test]
    public async Task Test_TryGetValidInvestmentResults_InvalidModelState_ShouldReturnBadRequest()
    {
        try
        {
            // Arrange
            this._CDBController.ModelState.AddModelError("Meses", GeneralHelper.MINIMUMMONTHS_MESSAGE);

            // Act
            var retorno = await this._CDBController.SolicitarCalculoInvestimento(new InvestimentoDto() { Valor = 1m, Meses = 1u });

            // Assert
            Assert.That(retorno, Is.TypeOf<BadRequestObjectResult>());
        }
        catch (Exception exception)
        {
            // Fail on exception
            Assert.Fail($"{GeneralHelper.ANOTHEREXCEPTION_MESSAGE}: {exception.Message}");
        }
    }
}

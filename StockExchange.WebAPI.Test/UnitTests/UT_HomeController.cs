using Microsoft.AspNetCore.Mvc;
using Moq;
using StockExchange.WebAPI.Controllers;
using StockExchange.WebAPI.Services;
using StockExchange.WebAPI.Test.Helpers;
using StockExchange.WebAPI.Test.UnitTests.Base;

namespace StockExchange.WebAPI.Test.UnitTests;

[TestFixture]
public sealed class UT_HomeController : UnitTestBase
{
    private Mock<IApplicationService> _ApplicationServiceMock;
    private HomeController _HomeController;

    [SetUp]
    public void SetUp()
    {
        this._ApplicationServiceMock = new Mock<IApplicationService>();
        this._HomeController = new HomeController(this._ApplicationServiceMock.Object);
    }

    [Test]
    public void Test_TryGetIndex_ShouldReturnOk()
    {
        try
        {
            // Act
            var retorno = this._HomeController.Index();

            // Assert
            Assert.That(retorno, Is.TypeOf<ContentResult>());
        }
        catch (Exception exception)
        {
            // Fail on exception
            Assert.Fail($"{GeneralHelper.ANOTHEREXCEPTION_MESSAGE}: {exception.Message}");
        }
    }
}

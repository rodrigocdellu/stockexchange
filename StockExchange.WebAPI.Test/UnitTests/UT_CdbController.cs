using StockExchange.WebAPI.Test.UnitTests.Base;

namespace StockExchange.WebAPI.Test.UnitTests;

[TestFixture]
public sealed class UT_CdbController : UnitTestBase
{
    [Test]
    public void Test_TryGetValidInvestmentResults()
    {
        try
        {
            //ToDo: Implement the test for CdbController
        }
        catch (Exception exception)
        {
            // Fail on exception
            Assert.Fail($"{UT_ApplicationService.TEST_ANOTHEREXCEPTION_MESSAGE}: {exception.Message}");
        }
    }
}

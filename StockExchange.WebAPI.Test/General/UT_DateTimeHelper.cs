﻿using StockExchange.WebAPI.Helpers;
using StockExchange.WebAPI.Test.Helpers;

namespace StockExchange.WebAPI.Test.General;

public class UT_DateTimeHelper
{
    [Test]
    public void Test_FusoHorarioBrazileiro()
    {
        // Load data
        var brasilianTimeZone = TestHelper.GetBrasilianTimeZone();
        var brasilianTimeZoneDisplayName = DateTime.UtcNow.PrepareTimeZoneForDockerization();

        // If there is no data, the test fail
        if (brasilianTimeZone == null)
            Assert.Fail();
        else
            Assert.That(brasilianTimeZone.DisplayName, Is.EqualTo(brasilianTimeZoneDisplayName));
    }
}

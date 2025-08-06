using StockExchange.WebAPI.Helpers;
using System.Runtime.InteropServices;

namespace StockExchange.WebAPI.Services;

public class ApplicationService : IApplicationService
{
    public string? TimeZone { get; set; }

    public DateTime StartupTime { get; set; }

    public string? FrameworkVersion { get; set;}

    public ApplicationService()
    {
        // Get the UTC now
        var utcNow = DateTime.UtcNow;

        // Set the values (considering docker container UTC 0)
        this.TimeZone = utcNow.PrepareTimeZoneForDockerization();
        this.StartupTime = utcNow.PrepareDateTimeForDockerization();
        this.FrameworkVersion = RuntimeInformation.FrameworkDescription;
    }
}

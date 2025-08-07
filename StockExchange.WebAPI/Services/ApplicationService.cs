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
        var targetTimeZone = utcNow.PrepareForDockerization();

        // Set the values (considering docker container UTC 0)
        this.TimeZone = targetTimeZone.DisplayName;
        this.StartupTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);
        this.FrameworkVersion = RuntimeInformation.FrameworkDescription;
    }
}

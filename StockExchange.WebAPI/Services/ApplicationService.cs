using System.Runtime.InteropServices;
using StockExchange.WebAPI.Helpers;

namespace StockExchange.WebAPI.Services;

public class ApplicationService : IApplicationService
{
    public string? TimeZone { get; set; }

    public DateTime StartupTime { get; set; }

    public string? FrameworkVersion { get; set;}

    public ApplicationService()
    {
        // Get the brasilian timezone
        var brazilTimeZone = ApplicationService.GetBrasilianTimeZone();

        // If the Brazil/SãoPaulo timezone was not found, used the UTC timezone
        if (brazilTimeZone == null)
        {
            this.TimeZone = TimeZoneInfo.Local.DisplayName;
            this.StartupTime = DateTime.UtcNow;
        }
        else
        {
            this.TimeZone = brazilTimeZone.DisplayName;
            this.StartupTime = DateTime.UtcNow.ConvertToLocalTime(brazilTimeZone.Id);
        }

        this.FrameworkVersion = RuntimeInformation.FrameworkDescription;
    }
    private static TimeZoneInfo? GetBrasilianTimeZone()
    {
        // Looking for a Brazil/SãoPaulo timezone due to Docker Container timezone
        return TimeZoneInfo.GetSystemTimeZones()
            .FirstOrDefault(internalTimeZone =>
                (internalTimeZone.Id.Contains("Brazil", StringComparison.OrdinalIgnoreCase) ||
                internalTimeZone.Id.Contains("E. South America", StringComparison.OrdinalIgnoreCase) ||
                internalTimeZone.Id.Contains("America/Sao_Paulo", StringComparison.OrdinalIgnoreCase) ||
                internalTimeZone.DisplayName.Contains("Brazil", StringComparison.OrdinalIgnoreCase) ||
                internalTimeZone.DisplayName.Contains("Sao Paulo", StringComparison.OrdinalIgnoreCase)) &&
                internalTimeZone.GetUtcOffset(DateTime.Now) == TimeSpan.FromHours(-3)
            );
    }
}

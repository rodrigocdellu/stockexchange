namespace StockExchange.WebAPI.Helpers;

public static class DateTimeHelper
{
    private static TimeZoneInfo? GetBrasilianTimeZone()
    {
        try
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
        catch
        {
            // Setup a Brazil/SãoPaulo timezone due to Docker Container timezone
            return TimeZoneInfo.CreateCustomTimeZone(
                id: "UTC-3",
                baseUtcOffset: TimeSpan.FromHours(-3),
                displayName: "(UTC-3) Horário de São Paulo",
                standardDisplayName: "UTC-3"
            );
        }        
    }
    
    public static DateTime ToSaoPauloDateTime(this DateTime utcDateTime)
    {
        // Get the target time zone
        var targetTimeZone = DateTimeHelper.GetBrasilianTimeZone();

        // If null
        if (targetTimeZone == null)
            // Return the current datetime
            return utcDateTime;
        else
            // Convert with the time zone
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, targetTimeZone);
    }

    public static string DisplayTimeZoneName(this DateTime utcDateTime)
    {
        // Get the target time zone
        var targetTimeZone = DateTimeHelper.GetBrasilianTimeZone();

        // If null
        if (targetTimeZone == null)
            // Return Empty
            return String.Empty;
        else
            // Return the display name
            return targetTimeZone.DisplayName;
    }    
}

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
    
    /// <summary>
    /// O fuso horário do Docker Container aspnet:6.0 é UTC 0.
    /// Portanto, ao se executar a aplicação a partir do container,
    /// é necessário fazer este ajuste. Caso contrário, o horário ficará negativo para o Brasil.
    /// Isso não afeta a execução fora do container docker.
    /// </summary>
    /// <returns>Um objeto DateTime UTC -3</returns>
    public static DateTime PrepareDateTimeForDockerization(this DateTime utcDateTime)
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

    /// <summary>
    /// O fuso horário do Docker Container aspnet:6.0 é UTC 0.
    /// Portanto, ao se executar a aplicação a partir do container,
    /// é necessário fazer este ajuste. Caso contrário, o horário ficará negativo para o Brasil.
    /// Isso não afeta a execução fora do container docker.
    /// </summary>
    /// <returns>O DisplayName do fuso horário de um objeto DateTime UTC -3</returns>
    public static string PrepareTimeZoneForDockerization(this DateTime utcDateTime)
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

using System.Collections.ObjectModel;

namespace StockExchange.WebAPI.Helpers;

public interface ITimeZoneProvider
{
    ReadOnlyCollection<TimeZoneInfo> GetSystemTimeZones();
}

public class SystemTimeZoneProvider : ITimeZoneProvider
{
    public ReadOnlyCollection<TimeZoneInfo> GetSystemTimeZones()
    {
        return TimeZoneInfo.GetSystemTimeZones();
    }
}

public static class DateTimeHelper
{
    private static TimeZoneInfo? GetBrasilianTimeZone(ITimeZoneProvider provider)
    {
        try
        {
            // Looking for a Brazil/SãoPaulo timezone due to Docker Container timezone
            return provider.GetSystemTimeZones()
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
    /// <param name="utcDateTime">>Data UTC original</param>
    /// <returns>Retorna o TimeZoneInfo de São Paulo da data UTC original</returns>
    public static TimeZoneInfo PrepareForDockerization(this DateTime utcDateTime)
    {
        // Return the Brazilian timezone
        return DateTimeHelper.GetBrasilianTimeZone(new SystemTimeZoneProvider())!;
    }
}

using System.Globalization;

namespace StockExchange.WebAPI.Helpers;

public static class MyDateTimeConverter
{
    public const string DEFAULTTIMEZONE = "UTC";

    public static DateTime ConvertToLocalTime(this DateTime utcDateTime, string localTimeZoneId)
    {
        try
        {
            // Get the target time Zone
            TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(localTimeZoneId);

            // Convert the time zone
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, targetTimeZone);    
        }
        catch (Exception exception)
        {
            throw new Exception(String.Format(CultureInfo.InvariantCulture, "Was not possible to convert from {0:yyyy-MM-dd hh:mm:ss} to {1} Time Zone ID.", utcDateTime, localTimeZoneId), exception);
        }
    }
}

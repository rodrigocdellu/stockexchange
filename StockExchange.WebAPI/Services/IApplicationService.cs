namespace StockExchange.WebAPI.Services;

public interface IApplicationService
{
    string? TimeZone { get; set; }

    DateTime StartupTime { get; set; }

    string? FrameworkVersion { get; set; }
}

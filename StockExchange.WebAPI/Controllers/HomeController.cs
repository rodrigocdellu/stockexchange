using Microsoft.AspNetCore.Mvc;
using StockExchange.WebAPI.Services;

namespace StockExchange.WebAPI.Controllers;

[ApiController]
[Route("/")]
public sealed class HomeController : ControllerBase
{
    private readonly IApplicationService _ApplicationService;

    public HomeController(IApplicationService applicationService)
    {
        this._ApplicationService = applicationService;
    }

    [HttpGet]
    public ContentResult Index()
    {
        var timezone = this._ApplicationService.TimeZone;
        var now = this._ApplicationService.StartupTime;
        var version = this._ApplicationService.FrameworkVersion;
        var uptime = now - this._ApplicationService.StartupTime;

        string uptimeFormatted = $"{(int)uptime.TotalHours:D2}:{uptime.Minutes:D2}:{uptime.Seconds:D2}";

        var html = $@"
            <!DOCTYPE html>
            <html>
                <head>
                    <title>Status da StockExchange.WebAPI</title>
                    <style>
                        :root {{
                            --primary-white: #ffffff;
                            --secondary-white: #e6f0fa;
                            --primary-blue: #1e4f91;
                            --secondary-blue: #003366;
                            --primary-black: #1a1a1a;
                        }}
                        body {{
                            margin: 0;
                            padding: 0;
                            height: 100vh;
                            display: flex;
                            justify-content: center;
                            align-items: center;
                            background-color: var(--secondary-white);                            
                            color: var(--primary-black);
                            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;                                                                                                                                            
                        }}
                        .card {{
                            padding: 2rem 3rem;
                            max-width: 500px;
                            background-color: var(--primary-white);
                            border-radius: 12px;
                            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
                            text-align: center;
                        }}
                        .link {{
                            color: var(--primary-black);
                            text-decoration: none;
                            font-size: 0.9rem;
                        }}
                        .link:hover {{
                            text-decoration: underline;
                        }}
                        h1 {{
                            margin-bottom: 1rem;
                            color: var(--primary-blue);
                        }}
                        p {{
                            margin: 0.5rem 0;
                            color: #333;
                            font-size: 1.1rem;
                        }}
                        strong {{
                            color: var(--secondary-blue);
                        }}
                    </style>
                </head>
                <body>
                    <div class=""card"">
                        <h1>Status da StockExchange.WebAPI</h1>
                        <p><strong>Fuso Horário:</strong> {timezone}</p>
                        <p><strong>Data de Início:</strong> {now:yyyy-MM-dd HH:mm:ss}</p>
                        <p><strong>Versão do .NET:</strong> {version}</p>
                        <p><strong>Uptime:</strong> <span id=""uptime"">{uptimeFormatted}</span> (hh:mm:ss)</p>
                        <p>
                            <br />
                            <a class=""link"" target=""_blank"" href=""/swagger/index.html"">Go to <strong>Swagger</strong> to check out the StockExchange.WebAPI</a>
                            <br />
                            <a class=""link"" target=""_blank"" href=""https://github.com/rodrigocdellu/stockexchange.back-end"">For <strong>More Information</strong> to go my GitHub</a>
                        </p>
                    </div>

                    <script>
                        // Function to calculate uptime dynamically
                        var startupTime = new Date('{this._ApplicationService.StartupTime:yyyy-MM-ddTHH:mm:ss}').getTime();

                        setInterval(function() {{
                            var now = new Date().getTime();
                            var uptime = now - startupTime;

                            var hours = Math.floor(uptime / (1000 * 60 * 60));
                            var minutes = Math.floor((uptime % (1000 * 60 * 60)) / (1000 * 60));
                            var seconds = Math.floor((uptime % (1000 * 60)) / 1000);

                            var uptimeFormatted = String(hours).padStart(2, '0') + "":"" +
                                                  String(minutes).padStart(2, '0') + "":"" +
                                                  String(seconds).padStart(2, '0');

                            // Updates the uptime element on the page
                            document.getElementById('uptime').textContent = uptimeFormatted;
                        }}, 1000);  // Updates every second

                        // Function to reload the page every 5 minutes (300000 ms)
                        setInterval(function() {{
                            location.reload();
                        }}, 300000);  // Reloads the page every 5 minutes
                    </script>
                </body>
            </html>";

        return Content(html, "text/html; charset=utf-8");
    }
}

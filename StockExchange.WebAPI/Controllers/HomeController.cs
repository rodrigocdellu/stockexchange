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
                    <meta charset=""utf-8"">
                    <title>StockExchange.WebAPI</title>
                    <base href=""/"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
                    <style>
                        :root {{
                            --primary-white: #ffffff;          /* Mantém branco */
                            --secondary-white: #ede7f6;        /* Deep Purple 50 - Fundo lavanda bem claro */
                            --primary-color: #5a2c8f;            /* Tom central personalizado - Base roxa desejada */
                            --secondary-color: #7e57c2;          /* Deep Purple 400 - Um pouco mais claro e vibrante */
                            --primary-black: #311b53;          /* Deep Purple 900 - Roxo muito escuro */
                            --font-color: #333333;             /* Cinza - Tom da fonte */
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
                            color: var(--primary-color);
                        }}
                        p {{
                            margin: 0.5rem 0;
                            color: var(--font-color);
                            font-size: 1.1rem;
                        }}
                        strong {{
                            color: var(--secondary-color);
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

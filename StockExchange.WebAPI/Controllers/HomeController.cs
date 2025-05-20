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
            <html lang=""en"">
                <head>
                    <meta charset=""utf-8"">
                    <title>StockExchange.WebAPI</title>
                    <base href=""/"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
                    <link rel=""icon"" type=""image/x-icon"" href=""/public/favicon.ico"">
                    <style>
                        :root {{
                            --primary-white: #ffffff;          /* Mantém branco */
                            --secondary-white: #ede7f6;        /* Deep Purple 50 - Fundo lavanda bem claro */
                            --primary-color: #5a2c8f;          /* Tom Central Personalizado - Base roxa desejada */
                            --secondary-color: #7e57c2;        /* Deep Purple 400 - Um pouco mais claro e vibrante */
                            --primary-black: #311b53;          /* Deep Purple 900 - Roxo muito escuro */
                            --font-color: #333333;             /* Cinza - Tom da fonte */
                        }}
                        .card, .grid {{
                            box-shadow: 0 4px 8px rgba(90, 44, 143, 0.1);
                            border-radius: 12px;
                        }}
                        .card {{
                            padding: 2rem 3rem;
                            max-width: 500px;
                            background-color: var(--primary-white);                                                        
                        }}
                        .grid {{
                            padding: 0.9rem 0.9rem;
                            display: grid;
                            grid-template-columns: max-content max-content;
                            row-gap: 0.5rem;
                            column-gap: 0.5rem;
                            justify-content: center;
                            border: 1px dashed var(--primary-color);
                        }}
                        .grid dt, .grid dd {{
                            margin: 0;
                            display: block;
                        }}
                        .grid dt {{
                            text-align: right;
                            font-weight: bold;
                            color: var(--secondary-color);
                        }}
                        .grid dd {{
                            text-align: left;
                        }}
                        .link {{
                            text-decoration: none;
                            color: var(--font-color);
                        }}
                        .link:hover {{
                            text-decoration: underline;
                        }}
                        p, figure {{
                            font-size: 0.9rem;
                        }}
                        body {{
                            margin: 0;
                            padding: 0;
                            height: 100vh;
                            display: flex;
                            justify-content: center;
                            align-items: center;
                            text-align: center;
                            background-color: var(--secondary-white);
                            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                            color: var(--font-color);
                        }}
                        h1 {{
                            margin: 6px 0 0 0;
                            color: var(--primary-color);
                        }}
                        strong {{
                            color: var(--secondary-color);
                        }}
                        figure figcaption {{
                            margin-bottom: 0.3rem;
                        }}
                    </style>
                </head>
                <body>
                    <div class=""card"">
                        <h1>StockExchange.WebAPI</h1>
                        <dl class=""grid"">
                            <dt>Time Zone:</dt><dd>{timezone}</dd>
                            <dt>Start Date:</dt><dd>{now:yyyy-MM-dd HH:mm:ss}</dd>
                            <dt>.NET Version:</dt><dd>{version}</dd>
                            <dt>Uptime:</dt><dd><span id=""uptime"">{uptimeFormatted}</span> (hh:mm:ss)</dd>
                        </dl>
                        <p>
                            <a class=""link"" target=""_blank"" href=""/swagger/index.html"">Go to <strong>Swagger</strong> to check out the StockExchange.WebAPI</a>
                            <br />
                            <a class=""link"" target=""_blank"" href=""https://github.com/rodrigocdellu/stockexchange.back-end"">For <strong>More Information</strong> to go my GitHub</a>
                        </p>
                        <figure>
                            <figcaption>Developed With</figcaption>
                            <a class=""link"" target=""_blank"" href=""https://dotnet.microsoft.com/pt-br/download/dotnet/6.0""><img src=""/public/DotNet.png"" alt="".NET"" /></a>
                            &nbsp;&nbsp;
                            <a class=""link"" target=""_blank"" href=""https://learn.microsoft.com/pt-br/dotnet/csharp/whats-new/csharp-version-history""><img src=""/public/CSharp.png"" alt=""C#"" /></a>
                        </figure>
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

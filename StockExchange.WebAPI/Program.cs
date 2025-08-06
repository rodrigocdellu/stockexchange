using FluentValidation; // 2025/04/30 - Required for Fluent Validation
using FluentValidation.AspNetCore; // 2025/04/30 - Required for Fluent Validation
using Microsoft.Extensions.FileProviders; // 2025/05/15 - Enable static files in the Public folder
using Microsoft.OpenApi.Models; // 2025/04/20 - Required for Dependency Injection (IoC)
using StockExchange.WebAPI.DTOs;
using StockExchange.WebAPI.Services;
using StockExchange.WebAPI.Validators;

const string POLICYFORCORS = "StockExchangePolicy"; // 2025/04/20 - Define the policy name
const string TERMSOFSERVICEURI = "https://github.com/rodrigocdellu"; // 2025/04/23 - SonarQube - Refactor your code not to use hardcoded absolute paths or URIs

// Create the application builder
var builder = WebApplication.CreateBuilder(args);

// Add services to the container. Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers(); // 2025/04/20 - Add controllers for automatic mapping
builder.Services.AddFluentValidationAutoValidation(); // 2025/04/30 - Required for Fluent Validation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => { // 2025/04/20 - For Swagger to run in the production environment
    options.SwaggerDoc("v1", new OpenApiInfo{
        Version = "v1",
        Title = "StockExchange.WebAPI",
        Description = "For StockExchange usage",
        TermsOfService = new Uri(TERMSOFSERVICEURI)
    });
});
builder.Services.AddTransient<IValidator<InvestimentoDto>, InvestimentoValidator>(); // 2025/04/30 - Required for Fluent Validation
builder.Services.AddSingleton<IApplicationService, ApplicationService>(); // 2025/04/22 - To deal with application information
builder.Services.AddTransient<ICdbService, CdbService>(); // 2025/04/22 - Add the Dependency Injection (IoC)

// 2025/04/20 - Add CORS with a policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(POLICYFORCORS, policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Build the application
var app = builder.Build();

// 2025/05/15 - Enable static files in the Public folder
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Public")),
    RequestPath = "/public"
});

app.UseSwagger(); // 2025/04/20 - For Swagger to run in the production environment
app.UseSwaggerUI(); // 2025/04/20 - For Swagger to run in the production environment
app.UseCors(POLICYFORCORS); // 2025/04/20 - Use CORS with the policy created
app.MapControllers(); // 2025/04/20 - Map the application controllers

// Run the application
await app.RunAsync();

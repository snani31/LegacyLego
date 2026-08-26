using LegacyLego.Application;
using LegacyLego.Infrastructure;
using LegacyLego.Presentation.Authentication.Endpoints;
using LegacyLego.Presentation.Authentication.Extensions;
using LegacyLego.Presentation.Middleware;
using LegacyLego.Presentation.OpenApi;
using LegacyLego.Presentation.Orders;
using LegacyLego.Presentation.Payments;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);

builder.Logging.ClearProviders();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

Serilog.Debugging.SelfLog.Enable(Console.Error);

try
{
    Log.Information("Запуск приложения LegacyLego...");

    builder.Services.ConfigureHttpJsonOptions(options =>
    {
        // превращает целочисленный указатель enum в строковое представление значения
        options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

    builder.Services.AddApplication()
        .AddInfrastructure(configuration)
        .AddPresentationOpenApi(configuration)
        .AddWebAuthentication();

    builder.Services.AddExceptionHandler<DynamicGlobalExceptionHandler>();
    builder.Services.AddProblemDetails();
    builder.Services.AddHealthChecks();

    var app = builder.Build();

    app.UseExceptionHandler(); // стоит самый первый в пайплайне

    app.UseForwardedHeaders(new ForwardedHeadersOptions // для Nginx
    {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
    });

    if (app.Environment.IsDevelopment())
        app.MapPresentationDocumentation();

    app.UseStaticFiles();

    app.UseAuthentication(); // Кто ты? (Расшифровываем токен)
    app.UseAuthorization();  // Что тебе можно? (Проверяем права)

    app.MapHealthChecks("/healthz");

    app.MapOrdersEndpoints();
    app.MapPaymentEndpoints();

    app.MapAuthenticationEndpoints();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Приложение LegacyLego аварийно завершило работу во время запуска");
}
finally
{
    Log.CloseAndFlush(); // Гарантирует, что все логи из буфера долетят до инфраструктурной базы логгов перед закрытием
}
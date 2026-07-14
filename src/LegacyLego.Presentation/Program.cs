using LegacyLego.Application;
using LegacyLego.Infrastructure;
using LegacyLego.Presentation.Middleware;
using LegacyLego.Presentation.OpenApi;
using LegacyLego.Presentation.Orders;
using LegacyLego.Presentation.Payments;
using Scalar.AspNetCore;
using Serilog;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), false);

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
        .AddPresentationOpenApi();

    builder.Services.AddExceptionHandler<DynamicGlobalExceptionHandler>();
    builder.Services.AddProblemDetails();

    var app = builder.Build();

    app.UseExceptionHandler(); // стоит самый первый в пайплайне

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();

        app.MapScalarApiReference("/docs/scalar", options =>
        {
            options.WithTitle("LegacyLego Documentation")
                .WithTheme(ScalarTheme.DeepSpace)
                .WithClassicLayout()
                .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
        });
    }

    app.UseStaticFiles();

    app.MapOrdersEndpoints();
    app.MapPaymentEndpoints();

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
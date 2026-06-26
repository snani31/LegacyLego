using LegacyLego.Application;
using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Infrastructure;
using LegacyLego.Infrastructure.Services;
using LegacyLego.Presentation.Middleware;
using LegacyLego.Presentation.OpenApi;
using LegacyLego.Presentation.Orders;
using LegacyLego.Presentation.Payments;
using Scalar.AspNetCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), false);

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
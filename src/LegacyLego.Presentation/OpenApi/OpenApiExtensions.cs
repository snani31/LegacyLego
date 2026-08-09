using LegacyLego.Presentation.OpenApi.Options;
using LegacyLego.Presentation.OpenApi.Transformers;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;

namespace LegacyLego.Presentation.OpenApi;

public static class OpenApiExtensions
{
    public const string ApiVersion = "v1";

    public static IServiceCollection AddPresentationOpenApi(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<OpenApiUiOptions>()
            .Bind(configuration.GetSection(OpenApiUiOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services.AddOpenApi(ApiVersion, options =>
        {
            options.AddDocumentTransformer<ApiMetadataTransformer>();
            options.AddDocumentTransformer<KeycloakOAuth2Transformer>();
        });
    }

    public static IEndpointRouteBuilder MapPresentationDocumentation(this IEndpointRouteBuilder app)
    {
        var uiOptions = app.ServiceProvider
            .GetRequiredService<IOptions<OpenApiUiOptions>>()
            .Value;

        app.MapOpenApi();

        app.MapScalarApiReference(uiOptions.RoutePrefix, options =>
        {
            options.WithTitle(uiOptions.Title)
                .WithTheme(ScalarTheme.DeepSpace)
                .WithClassicLayout()
                .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
                .AddAuthorizationCodeFlow("OAuth2", flow =>
                {
                    flow.ClientId = uiOptions.ClientId;
                });
        });

        return app;
    }

}

namespace LegacyLego.Presentation.OpenApi;

public static class OpenApiExtensions
{
    public const string ApiVersion = "v1";

    public static IServiceCollection AddPresentationOpenApi(this IServiceCollection services)
    {
        return services.AddOpenApi(ApiVersion, options =>
        {
            options.AddDocumentTransformer<ApiMetadataTransformer>();
        });
    }
}

using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace LegacyLego.Presentation.OpenApi.Transformers;

internal sealed class ApiMetadataTransformer : IOpenApiDocumentTransformer
{
    public Task TransformAsync(
        OpenApiDocument document,
        OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        document.Info.Title = "LegacyLego E-Commerce API";
        document.Info.Description = "Внутреннее API интернет-магазина конструкторов Lego. " +
                                     "Обеспечивает работу с заказами, корзиной и платежными шлюзами.";

        document.Info.Version = $"openapi.{context.DocumentName}";

        return Task.CompletedTask;
    }
}
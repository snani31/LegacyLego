using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi;

namespace LegacyLego.Presentation.OpenApi;

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

        // ПРАВИЛЬНЫЙ ВАРИАНТ: Берем имя документа из контекста .NET OpenAPI!
        // Если зарегистрирован документ "v1", то версия будет "v1"
        document.Info.Version = $"openapi.{context.DocumentName}";

        return Task.CompletedTask;
    }
}
using System.Security.Claims;

namespace LegacyLego.Presentation.JWT;

public static class TestJwtEndpoints
{
    public static IEndpointRouteBuilder MapTestJwtEndpoints(this IEndpointRouteBuilder app)
    {
        var ordersGroup = app.MapGroup("/jwt")
            .WithDisplayName("Test Jwt")
            .WithDescription("Тестирование функций Jwt и Keycloak")
            .WithTags("Jwt");

        ordersGroup.MapGet("/secret-data", GetSecretData)
            .RequireAuthorization();

        return app;
    }

    private static async Task<IResult> GetSecretData(
        ClaimsPrincipal user,
        CancellationToken ct)
    {
        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        var email = user.FindFirstValue(ClaimTypes.Email);

        return Results.Ok(new { Message = "Доступ разрешен!", UserId = userId, Email = email });
    }
}
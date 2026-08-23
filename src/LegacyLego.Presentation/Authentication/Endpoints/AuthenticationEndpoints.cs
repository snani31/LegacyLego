using LegacyLego.Infrastructure.Options;
using LegacyLego.Presentation.Authentication.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LegacyLego.Presentation.Authentication.Endpoints;

public static class AuthenticationEndpoints
{
    const string ROUTE_GROUP_NAME = "/auth";

    public static IEndpointRouteBuilder MapAuthenticationEndpoints(this IEndpointRouteBuilder app)
    {
        var jwtGroup = app.MapGroup(ROUTE_GROUP_NAME)
            .WithDisplayName("Authentication")
            .WithDescription("Авторизация, регистрация и выгрузка токенов")
            .WithTags("Auth");

        jwtGroup.MapGet("/login", GetLogin);
        jwtGroup.MapGet("/register", GetPublicRegistration);
        jwtGroup.MapGet("/callback", GetCallback);
        jwtGroup.MapGet("/logout", GetLogout);

        return app;
    }

    // стандартная авторизация OIDC
    private static IResult GetLogin(
        IOptions<KeycloakOptions> keycloakOptions,
        HttpContext httpContext)
    {
        var options = keycloakOptions.Value;
        var baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
        var redirectUri = $"{baseUrl}{ROUTE_GROUP_NAME}/callback";

        // генерация PKCE
        var (codeVerifier, codeChallenge) = PkceGenerator.GeneratePair();

        // Сохранение verifier в временную HttpOnly куку
        httpContext.Response.Cookies.Append("pkce_verifier", codeVerifier, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Expires = DateTimeOffset.UtcNow.AddMinutes(5)
        });

        var loginUrl = $"{options.AuthorizationEndpoint}" +
            $"?client_id={Uri.EscapeDataString(options.PublicClientId)}" +
            $"&redirect_uri={Uri.EscapeDataString(redirectUri)}" +
            $"&response_type=code" +
            $"&scope=openid" + 
            $"&code_challenge={codeChallenge}" +
            $"&code_challenge_method=S256";

        return Results.Redirect(loginUrl);
    }

    // Форма регистрации (с принудительным открытием регистрации через prompt=create)
    private static IResult GetPublicRegistration(
        IOptions<KeycloakOptions> keycloakOptions,
        HttpContext httpContext)
    {
        var options = keycloakOptions.Value;
        var baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
        var redirectUri = $"{baseUrl}{ROUTE_GROUP_NAME}/callback";

        var registrationUrl = $"{options.AuthorizationEndpoint}" +
            $"?client_id={Uri.EscapeDataString(options.PublicClientId)}" +
            $"&redirect_uri={Uri.EscapeDataString(redirectUri)}" +
            $"&response_type=code" +
            $"&scope=openid" +
            $"&prompt=create";

        return Results.Redirect(registrationUrl);
    }

    // Общий колбэк для получения токенов в виде JSON в обмен на код авторизации
    private static async Task<IResult> GetCallback(
        [FromQuery] string code,
        IOptions<KeycloakOptions> keycloakOptions,
        IHttpClientFactory httpClientFactory,
        HttpContext httpContext,
        CancellationToken ct)
    {

        // Считать сохраненный verifier из куки
        if (!httpContext.Request.Cookies.TryGetValue("pkce_verifier", out var codeVerifier))
        {
            return Results.BadRequest("PKCE verifier is missing or expired.");
        }
        // Удалить куку после считывания
        httpContext.Response.Cookies.Delete("pkce_verifier");

        var options = keycloakOptions.Value;
        var baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
        var redirectUri = $"{baseUrl}{ROUTE_GROUP_NAME}/callback";

        var client = httpClientFactory.CreateClient();

        var tokenEndpoint = $"{options.RealmUrl}/protocol/openid-connect/token";
        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["grant_type"] = "authorization_code",
            ["client_id"] = options.PublicClientId,
            ["code"] = code,
            ["redirect_uri"] = redirectUri,
            ["code_verifier"] = codeVerifier
        });

        var response = await client.PostAsync(tokenEndpoint, content, ct);
        var json = await response.Content.ReadAsStringAsync(ct);

        return Results.Content(json, "application/json");
    }

    // Завершение SSO-сессии в Keycloak
    private static IResult GetLogout(
        IOptions<KeycloakOptions> keycloakOptions,
        HttpContext httpContext)
    {
        var options = keycloakOptions.Value;
        var baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
        var postLogoutRedirectUri = $"{baseUrl}/docs/scalar";

        var logoutUrl = $"{options.RealmUrl}/protocol/openid-connect/logout" +
            $"?client_id={Uri.EscapeDataString(options.PublicClientId)}" +
            $"&post_logout_redirect_uri={Uri.EscapeDataString(postLogoutRedirectUri)}";

        return Results.Redirect(logoutUrl);
    }
}
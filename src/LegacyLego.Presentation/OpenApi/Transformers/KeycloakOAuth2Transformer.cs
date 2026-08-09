using LegacyLego.Infrastructure.Options;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;

namespace LegacyLego.Presentation.OpenApi.Transformers;

internal sealed class KeycloakOAuth2Transformer : IOpenApiDocumentTransformer
{
    private readonly JwtOptions _jwtOptions;

    public KeycloakOAuth2Transformer(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public Task TransformAsync(
        OpenApiDocument document,
        OpenApiDocumentTransformerContext context,
        CancellationToken cancellationToken)
    {
        var authority = _jwtOptions.Authority;

        var securityScheme = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Description = "Keycloak OpenID Connect / OAuth 2.0",
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri($"{authority}/protocol/openid-connect/auth"),
                    TokenUrl = new Uri($"{authority}/protocol/openid-connect/token"),
                    Scopes = new Dictionary<string, string>
                    {
                        { "openid", "OpenID Connect" },
                        { "profile", "Профиль пользователя" },
                        { "email", "Email пользователя" }
                    }
                }
            }
        };

        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
        document.Components.SecuritySchemes["OAuth2"] = securityScheme;

        var schemeReference = new OpenApiSecuritySchemeReference("OAuth2");
        var requirement = new OpenApiSecurityRequirement
        {
            [schemeReference] = []
        };

        if (document.Paths != null)
        {
            foreach (var path in document.Paths.Values)
            {
                if (path?.Operations == null) continue;

                foreach (var operation in path.Operations.Values)
                {
                    if (operation == null) continue;

                    operation.Security ??= new List<OpenApiSecurityRequirement>();
                    operation.Security.Add(requirement);
                }
            }
        }

        return Task.CompletedTask;
    }
}
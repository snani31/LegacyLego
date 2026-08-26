using LegacyLego.Infrastructure.Options;
using LegacyLego.Presentation.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LegacyLego.Presentation.Extensions;

internal sealed class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly JwtOptions _jwtOptions;
    private readonly KeycloakOptions _keycloakOptions;

    public ConfigureJwtBearerOptions(
        IOptions<JwtOptions> jwtOptions,
        IOptions<KeycloakOptions> keycloakOptions)
    {
        _jwtOptions = jwtOptions.Value;
        _keycloakOptions = keycloakOptions.Value;
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        if (name == JwtBearerDefaults.AuthenticationScheme)
        {
            Configure(options);
        }
    }

    public void Configure(JwtBearerOptions options)
    {
        options.Authority = _jwtOptions.Authority;
        options.RequireHttpsMetadata = _jwtOptions.RequireHttpsMetadata;
        options.MapInboundClaims = false;

        options.BackchannelHttpHandler = new KeycloakDockerBackchannelHandler(
            _keycloakOptions.PublicBaseUrl,   // "http://localhost/auth"
            _keycloakOptions.InternalBaseUrl  // "http://legacy-lego-keycloak:8080/auth"
        );

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _jwtOptions.ValidIssuer,
            ValidateAudience = false,
            RoleClaimType = "roles"
        };
    }
}
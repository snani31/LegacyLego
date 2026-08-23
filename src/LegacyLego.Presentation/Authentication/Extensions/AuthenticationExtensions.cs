using LegacyLego.Presentation.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace LegacyLego.Presentation.Authentication.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddWebAuthentication(this IServiceCollection services)
    {
        services.ConfigureOptions<ConfigureJwtBearerOptions>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.AddAuthorization();

        return services;
    }
}
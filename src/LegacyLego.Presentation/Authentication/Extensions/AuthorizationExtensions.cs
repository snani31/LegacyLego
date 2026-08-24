using LegacyLego.Presentation.Authentication.Common;

namespace LegacyLego.Presentation.Authentication.Extensions;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddApplicationAuthorization(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy(AuthConstants.Policies.ClientPolicy, policy =>
                policy.RequireRole(AuthConstants.Roles.Client))
            .AddPolicy(AuthConstants.Policies.AdminPolicy, policy =>
                policy.RequireRole(AuthConstants.Roles.Admin));

        return services;
    }
}
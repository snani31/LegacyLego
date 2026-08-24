using LegacyLego.Presentation.Authentication.Common;

namespace LegacyLego.Presentation.Authentication.Extensions;

public static class EndpointAuthorizationExtensions
{
    public static TBuilder RequireClientAuthorization<TBuilder>(this TBuilder builder)
        where TBuilder : IEndpointConventionBuilder
    {
        return builder.RequireAuthorization(AuthConstants.Policies.ClientPolicy);
    }

    public static TBuilder RequireAdminAuthorization<TBuilder>(this TBuilder builder)
        where TBuilder : IEndpointConventionBuilder
    {
        return builder.RequireAuthorization(AuthConstants.Policies.AdminPolicy);
    }
}
using LegacyLego.Presentation.Authentication.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace LegacyLego.IntegrationTests.Infrastructure.Authentication;

public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
	public const string SchemeName = "TestScheme";
	public static readonly Guid TestUserId = Guid.Parse("00000000-0000-0000-0000-000000000001");

	// Константы для заголовков
	public const string RoleHeader = "X-Test-Role";
	public const string UserIdHeader = "X-Test-UserId";

	public TestAuthHandler(
			IOptionsMonitor<AuthenticationSchemeOptions> options,
			ILoggerFactory logger,
			UrlEncoder encoder) : base(options, logger, encoder) { }

	protected override Task<AuthenticateResult> HandleAuthenticateAsync()
	{
		// Нет Authorization заголовка -> 401 Unauthorized
		if (!Request.Headers.ContainsKey("Authorization"))
		{
			return Task.FromResult(AuthenticateResult.NoResult());
		}

		var role = Request.Headers.TryGetValue(RoleHeader, out var roleValues)
				? roleValues.ToString()
				: AuthConstants.Roles.Client;

		var userId = Request.Headers.TryGetValue(UserIdHeader, out var userValues) && Guid.TryParse(userValues, out var parsedGuid)
				? parsedGuid
				: TestUserId;

		var claims = new[]
		{
						new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
						new Claim(ClaimTypes.Name, $"testuser_{userId}"),
						new Claim(ClaimTypes.Email, "testuser@legacylego.local"),
						new Claim(ClaimTypes.GivenName, "Test"),
						new Claim(ClaimTypes.Surname, "User"),
						new Claim(ClaimTypes.MobilePhone, "+1234567890"),
						new Claim("created_at", DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString()),
						new Claim(ClaimTypes.Role, role)
				};

		var identity = new ClaimsIdentity(claims, SchemeName);
		var principal = new ClaimsPrincipal(identity);

		return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal, SchemeName)));
	}
}

public static class TestAuthExtensions
{
	public static IServiceCollection AddTestAuth(this IServiceCollection services)
	{
		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = TestAuthHandler.SchemeName;
			options.DefaultChallengeScheme = TestAuthHandler.SchemeName;
		})
		.AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.SchemeName, _ => { });

		return services;
	}
}

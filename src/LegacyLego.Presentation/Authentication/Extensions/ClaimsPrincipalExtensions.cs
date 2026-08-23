using LegacyLego.Application.Abstractions.ExternalServices;
using LegacyLego.Application.Dto;
using LegacyLego.Domain.Shared;
using System.Security.Claims;

namespace LegacyLego.Presentation.Authentication.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user) 
    {
        var sub = user.FindFirstValue(ClaimTypes.NameIdentifier)
               ?? user.FindFirstValue("sub")
               ?? throw new InvalidOperationException("Отказ маппинга: в JWT отсутствует обязательный клейм 'sub'.");

        if (!Guid.TryParse(sub, out var userId))
            throw new InvalidOperationException($"Отказ маппинга: клейм 'sub' имеет невалидный формат GUID: '{sub}'.");

        return userId;
    }

    public static ExternalUserProfile ToExternalUserProfile(this ClaimsPrincipal user)
    {
        var userId = GetUserId(user);

        var email = user.FindFirstValue(ClaimTypes.Email)
                 ?? user.FindFirstValue("email")
                 ?? throw new InvalidOperationException("Отказ маппинга: в JWT отсутствует обязательный клейм 'email'.");

        var username = user.FindFirstValue("preferred_username")
                    ?? user.FindFirstValue(ClaimTypes.Name)
                    ?? throw new InvalidOperationException("Отказ маппинга: в JWT отсутствует обязательный клейм 'username'.");

        var firstName = user.FindFirstValue(ClaimTypes.GivenName)
                     ?? user.FindFirstValue("given_name");

        var lastName = user.FindFirstValue(ClaimTypes.Surname)
                    ?? user.FindFirstValue("family_name");

        var phoneNumber = user.FindFirstValue("phone_number");

        var createdAtClaim = user.FindFirstValue("created_at");
        var createdAtUtc = long.TryParse(createdAtClaim, out var unixMs)
            ? DateTimeOffset.FromUnixTimeMilliseconds(unixMs).UtcDateTime
            : throw new InvalidOperationException("Отказ маппинга: в JWT отсутствует обязательный клейм 'created_at'.");

        return new ExternalUserProfile(
            UserId: userId,
            Username: username,
            Email: email,
            FirstName: string.IsNullOrWhiteSpace(firstName) ? null : firstName,
            LastName: string.IsNullOrWhiteSpace(lastName) ? null : lastName,
            PhoneNumber: string.IsNullOrWhiteSpace(phoneNumber) ? null : phoneNumber,
            CreatedAtUtc: createdAtUtc
        );
    }
}
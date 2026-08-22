namespace LegacyLego.Application.Dto;

public record ExternalUserProfile(
    Guid UserId,
    string Username,
    string Email,
    string? FirstName,
    string? LastName,
    string? PhoneNumber,
    DateTime CreatedAtUtc
);
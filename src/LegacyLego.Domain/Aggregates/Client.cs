using LegacyLego.Domain.DomainEvents;
using LegacyLego.Domain.Enums;
using LegacyLego.Domain.Errors;
using LegacyLego.Domain.Shared;
using LegacyLego.Domain.ValueObjects;

namespace LegacyLego.Domain.Aggregates;

public class Client : AggregateRoot<ClientId>
{
    public string Username { get; }

    public string? FirstName { get; }

    public string? LastName { get; }

    public Email Email { get; }

    public PhoneNumber? PhoneNumber { get; }

    public ClientPreferences Preferences { get; }

    public DateTime CreatedAtUtc { get; }

    public Client(
        ClientId id,
        string username,
        ClientPreferences preferences,
        DateTime createdAtUtc,
        Email email,
        string? firstName = null,
        string? lastName = null,
        PhoneNumber? phoneNumber = null) : base(id)
    {
        Username = username;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        Preferences = preferences;
        CreatedAtUtc = createdAtUtc;
    }

    public static Result<Client> Create(
        ClientId id,
        string username,
        DateTime createdAt,
        Email email,
        ClientPreferences? preferences = null,
        string? firstName = null,
        string? lastName = null,
        PhoneNumber? phoneNumber = null)
    {
        ArgumentNullException.ThrowIfNullOrWhiteSpace(username, nameof(username));
        ArgumentNullException.ThrowIfNull(email, nameof(email));
        ArgumentNullException.ThrowIfNull(id, nameof(id));

        if (!String.IsNullOrWhiteSpace(firstName) && firstName.Length is < 1 or > 100)
            return Result<Client>.Failure(
                ClientErrors.GetCreateFirstNameInvalieLengthError(firstName));

        if (!String.IsNullOrWhiteSpace(lastName) && lastName.Length is < 1 or > 100)
            return Result<Client>.Failure(
                ClientErrors.GetCreateLastNameInvalieLengthError(lastName));

        if (createdAt == default) throw new ArgumentException("Date must be provided.", nameof(createdAt));
        if (createdAt.Kind is not DateTimeKind.Utc)
            return Result<Client>.Failure(
                ClientErrors.GetCreationTimeWasNotUtcError(createdAt));

        var client = new Client(
            id: id,
            username: username,
            createdAtUtc: createdAt,
            email: email,
            preferences: preferences ?? ClientPreferences.Default,
            firstName: firstName,
            lastName: lastName,
            phoneNumber: phoneNumber);

        client.Raise(new ClientCreatedDomainEvent(client.Id, client.Email.Value, createdAt));

        return Result<Client>.Success(client);
    }
}

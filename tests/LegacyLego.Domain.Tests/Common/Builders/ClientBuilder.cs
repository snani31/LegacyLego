namespace LegacyLego.Domain.Tests.Common.Builders;

internal class ClientBuilder
{
    private ClientId _id = ClientId.New();
    private string _username = "johndoe";
    private DateTime _createdAtUtc = DateTime.UtcNow;
    private Email _email = Email.Create("john.doe@example.com").Value;
    private ClientPreferences? _preferences = null;
    private string? _firstName = null;
    private string? _lastName = null;
    private PhoneNumber? _phoneNumber = null;

    public ClientBuilder WithId(ClientId id)
    {
        _id = id;
        return this;
    }

    public ClientBuilder WithNullId()
    {
        _id = null!;
        return this;
    }

    public ClientBuilder WithUsername(string username)
    {
        _username = username;
        return this;
    }

    public ClientBuilder WithNullOrEmptyUsername(string? username)
    {
        _username = username!;
        return this;
    }

    public ClientBuilder WithCreatedAt(DateTime createdAtUtc)
    {
        _createdAtUtc = createdAtUtc;
        return this;
    }

    public ClientBuilder WithEmail(Email email)
    {
        _email = email;
        return this;
    }

    public ClientBuilder WithNullEmail()
    {
        _email = null!;
        return this;
    }

    public ClientBuilder WithPreferences(ClientPreferences preferences)
    {
        _preferences = preferences;
        return this;
    }

    public ClientBuilder WithDefoultPreferences()
    {
        _preferences = ClientPreferences.Default;
        return this;
    }

    public ClientBuilder WithFirstName(string? firstName)
    {
        _firstName = firstName;
        return this;
    }

    public ClientBuilder WithLastName(string? lastName)
    {
        _lastName = lastName;
        return this;
    }

    public ClientBuilder WithPhoneNumber(PhoneNumber? phoneNumber)
    {
        _phoneNumber = phoneNumber;
        return this;
    }

    public Result<Client> BuildResult()
    {
        return Client.Create(
            _id,
            _username,
            _createdAtUtc,
            _email,
            _preferences,
            _firstName,
            _lastName,
            _phoneNumber);
    }

    public Client BuildValue() => BuildResult().Value;
}
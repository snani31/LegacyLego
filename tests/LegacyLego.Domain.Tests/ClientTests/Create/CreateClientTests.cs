namespace LegacyLego.Domain.Tests.ClientTests.Create;

public class CreateClientTests
{

    [Test]
    public async Task Create_WithMinimalValidData_ShouldReturnSuccessWithDefaultPreferences()
    {
        // Arrange
        var id = ClientId.New();
        var email = Email.Create("test@example.com").Value;
        var createdAt = DateTime.UtcNow;

        // Act
        var result = new ClientBuilder()
            .WithId(id)
            .WithUsername("clean_code")
            .WithEmail(email)
            .WithCreatedAt(createdAt)
            .WithPreferences(null!)
            .BuildResult();

        // Assert
        await Assert.That(result.IsSuccess).IsTrue();

        var client = result.Value;
        await Assert.That(client.Id).IsEqualTo(id);
        await Assert.That(client.Username).IsEqualTo("clean_code");
        await Assert.That(client.Email).IsEqualTo(email);
        await Assert.That(client.CreatedAtUtc).IsEqualTo(createdAt);
        await Assert.That(client.Preferences).IsEqualTo(ClientPreferences.Default);
        await Assert.That(client.FirstName).IsNull();
        await Assert.That(client.LastName).IsNull();
        await Assert.That(client.PhoneNumber).IsNull();
    }

    [Test]
    public async Task Create_WithFullValidData_ShouldMapAllPropertiesCorrectly()
    {
        // Arrange
        var id = ClientId.New();
        var email = Email.Create("full@example.com").Value;
        var phone = PhoneNumber.Create("+79991234567").Value;
        var preferences = ClientPreferences.Create(Language.Russian, Currency.Usd).Value;
        var createdAt = DateTime.UtcNow;

        // Act
        var result = new ClientBuilder()
            .WithId(id)
            .WithUsername("full_user")
            .WithEmail(email)
            .WithCreatedAt(createdAt)
            .WithPreferences(preferences)
            .WithFirstName("Ivan")
            .WithLastName("Ivanov")
            .WithPhoneNumber(phone)
            .BuildResult();

        // Assert
        await Assert.That(result.IsSuccess).IsTrue();

        var client = result.Value;
        await Assert.That(client.Id).IsEqualTo(id);
        await Assert.That(client.Username).IsEqualTo("full_user");
        await Assert.That(client.FirstName).IsEqualTo("Ivan");
        await Assert.That(client.LastName).IsEqualTo("Ivanov");
        await Assert.That(client.Email).IsEqualTo(email);
        await Assert.That(client.PhoneNumber).IsEqualTo(phone);
        await Assert.That(client.Preferences).IsEqualTo(preferences);
    }


    [Test]
    public async Task Create_WithNullId_ShouldThrowArgumentNullException()
    {
        var action = () => new ClientBuilder().WithNullId().BuildResult();

        await Assert.That(action).ThrowsExactly<ArgumentNullException>()
            .WithParameterName("id");
    }

    [Test]
    [Arguments(null)]
    [Arguments("")]
    [Arguments("   ")]
    public async Task Create_WithNullOrWhiteSpaceUsername_ShouldThrowArgumentException(string? invalidUsername)
    {
        var action = () => new ClientBuilder().WithUsername(invalidUsername!).BuildResult();

        await Assert.That(action).Throws<ArgumentException>();
    }

    [Test]
    public async Task Create_WithNullEmail_ShouldThrowArgumentNullException()
    {
        var action = () => new ClientBuilder().WithNullEmail().BuildResult();

        await Assert.That(action).ThrowsExactly<ArgumentNullException>()
            .WithParameterName("email");
    }

    [Test]
    public async Task Create_WithDefaultCreatedAt_ShouldThrowArgumentException()
    {
        var action = () => new ClientBuilder().WithCreatedAt(default).BuildResult();

        await Assert.That(action).ThrowsExactly<ArgumentException>()
            .WithParameterName("createdAt");
    }


    [Test]
    public async Task Create_WithNonUtcCreatedAt_ShouldReturnCreationTimeWasNotUtcError()
    {
        var localTime = DateTime.Now; 

        var result = new ClientBuilder().WithCreatedAt(localTime).BuildResult();

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code)
            .IsEqualTo(ClientErrors.GetCreationTimeWasNotUtcError(localTime).Code);
    }

    [Test]
    public async Task Create_WithFirstNameLongerThan100Chars_ShouldReturnInvalidLengthError()
    {
        var longFirstName = new string('a', 101);

        var result = new ClientBuilder().WithFirstName(longFirstName).BuildResult();

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code)
            .IsEqualTo(ClientErrors.GetCreateFirstNameInvalieLengthError(longFirstName).Code);
    }

    [Test]
    public async Task Create_WithLastNameLongerThan100Chars_ShouldReturnInvalidLengthError()
    {
        var longLastName = new string('a', 101);

        var result = new ClientBuilder().WithLastName(longLastName).BuildResult();

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code)
            .IsEqualTo(ClientErrors.GetCreateLastNameInvalieLengthError(longLastName).Code);
    }


    [Test]
    public async Task Create_OnSuccess_ShouldRaiseClientCreatedDomainEvent()
    {
        var id = ClientId.New();
        var email = Email.Create("event@example.com").Value;
        var createdAt = DateTime.UtcNow;

        var client = new ClientBuilder()
            .WithId(id)
            .WithEmail(email)
            .WithCreatedAt(createdAt)
            .BuildValue();

        await Assert.That(client.DomainEvents).Count().IsEqualTo(1);

        var domainEvent = client.DomainEvents.Single() as ClientCreatedDomainEvent;
        await Assert.That(domainEvent).IsNotNull();
        await Assert.That(domainEvent!.ClientId).IsEqualTo(id);
        await Assert.That(domainEvent.Email).IsEqualTo(email.Value);
    }
}

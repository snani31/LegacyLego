namespace LegacyLego.Domain.Tests.EmailTests.Create;

public class EmailCreateTests
{
    [Test]
    [Arguments("test@example.com", "test@example.com", "test", "example.com")]
    [Arguments("  USER@DOMAIN.COM  ", "user@domain.com", "user", "domain.com")]
    [Arguments("First.Last@Sub.Domain.org", "first.last@sub.domain.org", "first.last", "sub.domain.org")]
    public async Task Create_WithValidEmail_ShouldReturnSuccessAndSetCorrectProperties(
        string input,
        string expectedValue,
        string expectedLocalPart,
        string expectedDomain)
    {
        var result = Email.Create(input);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value.Value).IsEqualTo(expectedValue);
        await Assert.That(result.Value.LocalPart).IsEqualTo(expectedLocalPart);
        await Assert.That(result.Value.Domain).IsEqualTo(expectedDomain);
    }

    [Test]
    public async Task Create_WithNullValue_ShouldThrowArgumentNullException()
    {
        var action = () => Email.Create(null!);

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    [Arguments("")]
    [Arguments("   ")]
    public async Task Create_WithEmptyOrWhiteSpace_ShouldThrowArgumentException(string input)
    {
        var action = () => Email.Create(input);

        await Assert.That(action).Throws<ArgumentException>();
    }

    [Test]
    public async Task Create_WithValueExceeding256Characters_ShouldReturnTooLongError()
    {
        var longLocalPart = new string('a', 245);
        var longEmail = $"{longLocalPart}@example.com";

        var result = Email.Create(longEmail);

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code)
            .IsEqualTo(EmailErrors.CreateValueTooLongCode);
    }

    [Test]
    [Arguments("plainaddress")]
    [Arguments("#@%^%#$@#$@#.com")]
    [Arguments("@example.com")]
    [Arguments("Joe Smith <email@example.com>")]
    [Arguments("email.example.com")]
    [Arguments("email@example@example.com")]
    [Arguments("email@example.")]
    [Arguments("email@")]
    public async Task Create_WithInvalidRegexFormat_ShouldReturnRegexInvalidFormatError(string invalidEmail)
    {
        var result = Email.Create(invalidEmail);

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code)
            .IsEqualTo(EmailErrors.CreateValueRegexFailureCode);
    }
}
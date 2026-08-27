namespace LegacyLego.Domain.Tests.LanguageTests.FromCode;

public class LanguageFromCodeTests
{
    [Test]
    public async Task FromCode_WithValidENCode_ShouldReturnSuccess()
    {
        var result = Language.FromCode("EN-US");

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value).IsEqualTo(Language.English);
        await Assert.That(result.Value).IsSameReferenceAs(Language.English);
    }

    [Test]
    public async Task FromCode_WithUnknownValidCode_ShouldReturnNotSupported()
    {
        var result = Language.FromCode("ABC");

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error).Member(error => error.Code, name => name.EqualTo(LanguageErrors.NotSupportedCode));
    }

    [Test]
    [Arguments("")]
    [Arguments(" ")]
    public async Task FromCode_WithEmptyString_ShouldThrowArgumentNullException(string whiteSpace)
    {
        var exception = await Assert.That(() => Language.FromCode(whiteSpace)).ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task FromCode_WithNullCode_ShouldThrowArgumentNullException()
    {
        var exception = await Assert.That(() => Language.FromCode(null!)).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    [Arguments("RU-Ru")]
    [Arguments("Ru-RU")]
    [Arguments("ru-ru")]
    [Arguments("rU-RU")]
    [Arguments("RU-rU")]
    public async Task FromCode_WithLowerCaseValidCode_ShouldReturnSuccess(string inputLowerCode)
    {
        var result = Language.FromCode(inputLowerCode);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value).IsEqualTo(Language.Russian);
        await Assert.That(result.Value).IsSameReferenceAs(Language.Russian);
    }

    [Test]
    public async Task FromCode_WithValidUntrimmedCode_ShouldReturnSuccess()
    {
        var result = Language.FromCode("    RU-RU  ");

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value).IsEqualTo(Language.Russian);
        await Assert.That(result.Value).IsSameReferenceAs(Language.Russian);
    }

    [Test]
    [Arguments("RU-RU")]
    [Arguments("EN-US")]
    public async Task FromCode_ShouldReturnLanguageWithCorrectProperties(string code)
    {
        var result = Language.FromCode(code);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value.Code).IsEqualTo(code);
    }
}

using LegacyLego.Domain.Errors;
using LegacyLego.Domain.ValueObjects;
using TUnit.Core;

namespace LegacyLego.Domain.Tests.CurrencyTests;

public class CurrencyFromCodeTests
{
    [Test]
    public async Task FromCode_WithValidCodeUSD_ShouldReturnSuccess()
    {
        var result =  Currency.FromCode("USD");

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value).IsEqualTo(Currency.Usd);
        await Assert.That(result.Value).IsSameReferenceAs(Currency.Usd);
    }

    [Test]
    public async Task FromCode_WithUnknownValidCode_ShouldReturnNotSupported()
    {
        var result = Currency.FromCode("ABC");

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error).Member(error => error.Code,name => name.EqualTo(CurrencyErrors.NotSupportedCode));
    }

    [Test]
    public async Task FromCode_WithEmptyString_ShouldReturnWrongCodeLengthError()
    {
        var result = Currency.FromCode("");

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code).IsEqualTo(CurrencyErrors.WrongCodeLengthCode);
    }

    [Test]
    public async Task FromCode_WithInvalidCodeLength_ShouldReturnWrongCodeLengthError()
    {
        var result = Currency.FromCode("USDDD");

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error)
            .Member(error => error.Code, code => code.EqualTo(CurrencyErrors.WrongCodeLengthCode));
    }

    [Test]
    public async Task FromCode_WithNullCode_ShouldThrowArgumentNullException()
    {
        var exception = await Assert.That(() => Currency.FromCode(null!)).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    [Arguments("usd")]
    [Arguments("Usd")]
    [Arguments("uSd")]
    [Arguments("usD")]
    [Arguments("UsD")]
    public async Task FromCode_WithLowerCaseValidCode_ShouldReturnSuccess(string inputLowerCode)
    {
        var result = Currency.FromCode(inputLowerCode);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value).IsEqualTo(Currency.Usd);
        await Assert.That(result.Value).IsSameReferenceAs(Currency.Usd);
    }

    [Test]
    public async Task FromCode_WithValidUntrimmedCode_ShouldReturnSuccess()
    {
        var result = Currency.FromCode("    USD  ");

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value).IsEqualTo(Currency.Usd);
        await Assert.That(result.Value).IsSameReferenceAs(Currency.Usd);
    }

    [Test]
    public async Task FromCode_WithInvalidUntrimmedCode_ShouldReturnWrongCodeLengthError()
    {
        var result = Currency.FromCode("    USDDDDD  ");

        await Assert.That(result.IsSuccess).IsFalse();
        await Assert.That(result.Error.Code).IsEqualTo(CurrencyErrors.WrongCodeLengthCode);
    }

    [Test]
    [Arguments("U")]
    [Arguments("US")]
    public async Task FromCode_WithShortCode_ShouldReturnWrongCodeLengthError(string code)
    {
        var result = Currency.FromCode(code);

        await Assert.That(result.IsFailure).IsTrue();
        await Assert.That(result.Error.Code).IsEqualTo(CurrencyErrors.WrongCodeLengthCode);
    }

    [Test]
    [Arguments("USD", "$", 2)]
    [Arguments("RUB", "₽", 2)]
    [Arguments("EUR", "€", 2)]
    public async Task FromCode_ShouldReturnCurrencyWithCorrectProperties(string code,string symbol,int scale)
    {
        var result = Currency.FromCode(code);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value.Symbol).IsEqualTo(symbol);
        await Assert.That(result.Value.Scale).IsEqualTo(scale);
    }
}

using LegacyLego.Domain.Errors;
using LegacyLego.Domain.ValueObjects;
using TUnit.Assertions;
using TUnit.Assertions.Exceptions;
using TUnit.Assertions.Extensions;
using TUnit.Core;

namespace LegacyLego.Domain.Tests.CurrencyTests.FromCode;

public class CurrencyFromCodeTests
{
    [Test]
    public async Task FromCode_WithValidCodeUSD_ShouldReturnSuccess()
    {
        var result =  Currency.FromCode("USD");

        await Assert.That(result.IsSuccess).Is.True();
        await Assert.That(result.Value).Is.EqualTo(Currency.Usd);
        await Assert.That(result.Value).Is.SameReference(Currency.Usd);
    }

    [Test]
    public async Task FromCode_WithUnknownValidCode_ShouldReturnNotSupported()
    {
        var result = Currency.FromCode("ABC");

        await Assert.That(result.IsFailure).Is.True();
        await Assert.That(result.Error)
            .Has.Member(error => error.Code).EqualTo(CurrencyErrors.NotSupportedCode);
    }

    [Test]
    public async Task FromCode_WithEmptyString_ShouldReturnWrongCodeLengthError()
    {
        var result = Currency.FromCode("");

        await Assert.That(result.IsFailure).Is.True();
        await Assert.That(result.Error.Code).Is.EqualTo(CurrencyErrors.WrongCodeLengthCode);
    }

    [Test]
    public async Task FromCode_WithInvalidCodeLength_ShouldReturnWrongCodeLengthError()
    {
        var result = Currency.FromCode("USDDD");

        await Assert.That(result.IsFailure).Is.True();
        await Assert.That(result.Error)
            .Has.Member(error => error.Code).EqualTo(CurrencyErrors.WrongCodeLengthCode);
    }

    [Test]
    public async Task FromCode_WithNullCode_ShouldThrowArgumentNullException()
    {
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(async () =>
        {
            Currency.FromCode(null!);
        });

    }

    [DataDrivenTest]
    [Arguments("usd")]
    [Arguments("Usd")]
    [Arguments("uSd")]
    [Arguments("usD")]
    [Arguments("UsD")]
    public async Task FromCode_WithLowerCaseValidCode_ShouldReturnSuccess(string inputLowerCode)
    {
        var result = Currency.FromCode(inputLowerCode);

        await Assert.That(result.IsSuccess).Is.True();
        await Assert.That(result.Value).Is.EqualTo(Currency.Usd);
        await Assert.That(result.Value).Is.SameReference(Currency.Usd);
    }

    [Test]
    public async Task FromCode_WithValidUntrimmedCode_ShouldReturnSuccess()
    {
        var result = Currency.FromCode("    USD  ");

        await Assert.That(result.IsSuccess).Is.True();
        await Assert.That(result.Value).Is.EqualTo(Currency.Usd);
        await Assert.That(result.Value).Is.SameReference(Currency.Usd);
    }

    [Test]
    public async Task FromCode_WithInvalidUntrimmedCode_ShouldReturnWrongCodeLengthError()
    {
        var result = Currency.FromCode("    USDDDDD  ");

        await Assert.That(result.IsSuccess).Is.False();
        await Assert.That(result.Error.Code).Is.EqualTo(CurrencyErrors.WrongCodeLengthCode);
    }

    [DataDrivenTest]
    [Arguments("U")]
    [Arguments("US")]
    public async Task FromCode_WithShortCode_ShouldReturnWrongCodeLengthError(string code)
    {
        var result = Currency.FromCode(code);

        await Assert.That(result.IsFailure).Is.True();
        await Assert.That(result.Error.Code).Is.EqualTo(CurrencyErrors.WrongCodeLengthCode);
    }

    [DataDrivenTest]
    [Arguments("USD", "$", 2)]
    [Arguments("RUB", "₽", 2)]
    [Arguments("EUR", "€", 2)]
    public async Task FromCode_ShouldReturnCurrencyWithCorrectProperties(string code,string symbol,int scale)
    {
        var result = Currency.FromCode(code);

        await Assert.That(result.IsSuccess).Is.True();
        await Assert.That(result.Value.Symbol).Is.EqualTo(symbol);
        await Assert.That(result.Value.Scale).Is.EqualTo(scale);
    }
}

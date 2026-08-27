namespace LegacyLego.Domain.Tests.ClientPreferencesTests.Create;

public class ClientPreferencesCreateTests
{
    [Test]
    public async Task Create_WithValidLanguageAndCurrency_ShouldReturnSuccessWithCorrectCodes()
    {
        var language = Language.Russian;
        var currency = Currency.Usd;

        var result = ClientPreferences.Create(language, currency);

        await Assert.That(result.IsSuccess).IsTrue();
        await Assert.That(result.Value.LanguageCode).IsEqualTo(language.Code);
        await Assert.That(result.Value.CurrencyCode).IsEqualTo(currency.Code);
    }

    [Test]
    public async Task Create_WithNullLanguage_ShouldThrowArgumentNullException()
    {
        var action = () => ClientPreferences.Create(null!, Currency.Usd);

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task Create_WithNullCurrency_ShouldThrowArgumentNullException()
    {
        var action = () => ClientPreferences.Create(Language.Russian, null!);

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }
}

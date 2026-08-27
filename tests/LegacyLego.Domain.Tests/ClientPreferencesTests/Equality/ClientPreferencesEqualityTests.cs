namespace LegacyLego.Domain.Tests.ClientPreferencesTests.Equality;

public class ClientPreferencesEqualityTests
{
    [Test]
    public async Task Default_ShouldReturnPreferencesWithRussianLanguageAndRubCurrency()
    {
        var defaultPreferences = ClientPreferences.Default;

        await Assert.That(defaultPreferences.LanguageCode).IsEqualTo(Language.Russian.Code);
        await Assert.That(defaultPreferences.CurrencyCode).IsEqualTo(Currency.Rub.Code);
    }

    [Test]
    public async Task Equals_WithSameLanguageAndCurrencyCodes_ShouldBeTrue()
    {
        var pref1 = ClientPreferences.Create(Language.Russian, Currency.Rub).Value;
        var pref2 = ClientPreferences.Create(Language.Russian, Currency.Rub).Value;

        await Assert.That(pref1).IsEqualTo(pref2);
        await Assert.That(pref1 == pref2).IsTrue();
        await Assert.That(pref1 != pref2).IsFalse();
    }

    [Test]
    public async Task Equals_WithDifferentLanguageOrCurrency_ShouldBeFalse()
    {
        var pref1 = ClientPreferences.Create(Language.Russian, Currency.Rub).Value;
        var pref2 = ClientPreferences.Create(Language.Russian, Currency.Usd).Value;

        await Assert.That(pref1).IsNotEqualTo(pref2);
        await Assert.That(pref1 == pref2).IsFalse();
        await Assert.That(pref1 != pref2).IsTrue();
    }

    [Test]
    public async Task GetHashCode_ForEqualPreferences_ShouldBeSame()
    {
        var pref1 = ClientPreferences.Create(Language.Russian, Currency.Rub).Value;
        var pref2 = ClientPreferences.Create(Language.Russian, Currency.Rub).Value;

        await Assert.That(pref1.GetHashCode()).IsEqualTo(pref2.GetHashCode());
    }
}

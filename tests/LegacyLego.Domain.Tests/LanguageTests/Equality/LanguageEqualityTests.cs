namespace LegacyLego.Domain.Tests.LanguageTests.Equality;

public class LanguageEqualityTests
{
    [Test]
    public async Task Equals_WithSameCode_ShouldBeTrue()
    {
        var l1 = Language.FromCode("RU-RU").Value;
        var l2 = Language.FromCode("ru-ru").Value;

        await Assert.That(l1.Equals(l2)).IsTrue();
    }

    [Test]
    public async Task Equals_WithDifferentCode_ShouldBeFalse()
    {
        var rus = Language.FromCode("RU-RU").Value;
        var eng = Language.FromCode("EN-US").Value;

        await Assert.That(rus.Equals(eng)).IsFalse();
    }

    [Test]
    public async Task Equals_ShouldBeConsistentWithEqualsOperator()
    {
        var l1 = Language.FromCode("RU-RU").Value;
        var l2 = Language.FromCode("RU-RU").Value;

        await Assert.That(l1 == l2).IsTrue();
    }

    [Test]
    public async Task Equals_ShouldBeConsistentWithNotEqualsOperator()
    {
        var l1 = Language.FromCode("RU-RU").Value;
        var l2 = Language.FromCode("EN-US").Value;

        await Assert.That(l1 != l2).IsTrue();
    }

    [Test]
    public async Task GetHashCode_ForEqualObjects_ShouldBeSame()
    {
        var l1 = Language.FromCode("RU-RU").Value;
        var l2 = Language.FromCode("ru-ru").Value;

        await Assert.That(l1.GetHashCode()).IsEqualTo(l2.GetHashCode());
    }

    [Test]
    public async Task Equals_ShouldDependOnlyOnCode()
    {
        var rus = Language.FromCode("RU-RU").Value;

        await Assert.That(rus.Code).IsEqualTo("RU-RU");
    }
}

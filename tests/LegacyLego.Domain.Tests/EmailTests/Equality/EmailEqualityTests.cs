namespace LegacyLego.Domain.Tests.EmailTests.Equality;

public class EmailEqualityTests
{
    [Test]
    public async Task Equals_WithSameUnnormalizedValue_ShouldBeTrue()
    {
        var email1 = Email.Create("user@example.com").Value;
        var email2 = Email.Create("USER@EXAMPLE.COM").Value; // Проверяем с учетом нормализации

        await Assert.That(email1).IsEqualTo(email2);
        await Assert.That(email1 == email2).IsTrue();
        await Assert.That(email1 != email2).IsFalse();
    }

    [Test]
    public async Task Equals_WithDifferentValue_ShouldBeFalse()
    {
        var email1 = Email.Create("user1@example.com").Value;
        var email2 = Email.Create("user2@example.com").Value;

        await Assert.That(email1).IsNotEqualTo(email2);
        await Assert.That(email1 == email2).IsFalse();
        await Assert.That(email1 != email2).IsTrue();
    }

    [Test]
    public async Task GetHashCode_WithSameNormalizedValue_ShouldBeSame()
    {
        var email1 = Email.Create("user@domain.com").Value;
        var email2 = Email.Create("  USER@DOMAIN.COM ").Value;

        await Assert.That(email1.GetHashCode()).IsEqualTo(email2.GetHashCode());
    }

    [Test]
    public async Task ImplicitConversion_ToString_ShouldReturnCorrectStringValue()
    {
        var email = Email.Create("user@domain.com").Value;

        string rawEmail = email;

        await Assert.That(rawEmail).IsEqualTo("user@domain.com");
    }

    [Test]
    public async Task ToString_ShouldReturnValue()
    {
        var email = Email.Create("user@domain.com").Value;

        await Assert.That(email.ToString()).IsEqualTo("user@domain.com");
    }
}

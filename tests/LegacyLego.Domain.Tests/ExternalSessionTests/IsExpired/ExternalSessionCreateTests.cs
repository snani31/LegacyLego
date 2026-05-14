namespace LegacyLego.Domain.Tests.ExternalSessionTests;

public class ExternalSessionIsExpiredTests
{
    [Test]
    public async Task IsExpired_WithLowerUtc_ShouldReturnFalse()
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(60);
        var time = expiresAt.AddMinutes(-30);

        var session = ExternalSession.Create("id", "url", expiresAt).Value;
        
        await Assert.That(session.IsExpired(time)).IsFalse();
    }

    [Test]
    public async Task IsExpired_WithMinimalLowerUtc_ShouldReturnFalse()
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(60);
        var time = expiresAt.AddMicroseconds(-1);

        var session = ExternalSession.Create("id", "url", expiresAt).Value;

        await Assert.That(session.IsExpired(time)).IsFalse();
    }

    [Test]
    public async Task IsExpired_WithSameValues_ShouldReturnTrue()
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(60);

        var session = ExternalSession.Create("id", "url", expiresAt).Value;

        await Assert.That(session.IsExpired(expiresAt)).IsTrue();
    }

    [Test]
    public async Task IsExpired_WithBiggerUtc_ShouldReturnTrue()
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(60);
        var time = expiresAt.AddMinutes(30);

        var session = ExternalSession.Create("id", "url", expiresAt).Value;

        await Assert.That(session.IsExpired(time)).IsTrue();
    }

    [Test]
    public async Task IsExpired_WithMinimalBiggerUtc_ShouldReturnTrue()
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(60);
        var time = expiresAt.AddMicroseconds(1);

        var session = ExternalSession.Create("id", "url", expiresAt).Value;

        await Assert.That(session.IsExpired(time)).IsTrue();
    }

    [Test]
    public async Task IsExpired_WithLocalDateTimeKind_ShouldThrowInvariantViolationExceptionWithIsExpiredCompresionParameterIsNotUtcCode()
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(60);
        var local = DateTime.Now; 

        var session = ExternalSession.Create("id", "url", expiresAt).Value;

        var action = () => session.IsExpired(local);

        var exception = await Assert.That(action).ThrowsExactly<InvariantViolationException>();
        await Assert.That(exception!.Error.Code).EqualTo(ExternalSessionExceptionalErrors.IsExpiredCompressionParameterIsNotUtcCode);
    }

    [Test]
    public async Task IsExpired_WithUnspecifiedDateTimeKind_ShouldThrowInvariantViolationExceptionWithIsExpiredCompressionParameterIsNotUtcCode()
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(60);
        var unspecified = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

        var session = ExternalSession.Create("id", "url", expiresAt).Value;

        var action = () => session.IsExpired(unspecified);

        var exception = await Assert.That(action).ThrowsExactly<InvariantViolationException>();
        await Assert.That(exception!.Error.Code).EqualTo(ExternalSessionExceptionalErrors.IsExpiredCompressionParameterIsNotUtcCode);
    }

    [Test]
    public async Task IsExpired_ExpiresAtUtcShouldStayImmutable()
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(60);
        var time = expiresAt.AddMinutes(30);

        var session = ExternalSession.Create("id", "url", expiresAt).Value;

        await Assert.That(session.ExpiresAtUtc).IsEquivalentTo(expiresAt);

        session.IsExpired(time); 

        await Assert.That(session.ExpiresAtUtc).IsEquivalentTo(expiresAt);
    }
}
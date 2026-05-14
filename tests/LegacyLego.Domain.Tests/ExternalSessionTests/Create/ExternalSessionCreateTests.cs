namespace LegacyLego.Domain.Tests.ExternalSessionTests;

public class ExternalSessionCreateTests
{
    [Test]
    public async Task Create_WithValidValues_ShouldPreserve()
    {
        var id = "id";
        var url = "url";
        var time = DateTime.UtcNow.AddMinutes(60);

        var r = ExternalSession.Create(id, url, time);

        await Assert.That(r.IsSuccess).IsTrue();
        await Assert.That(r.Value)
            .Member(x => x.ExternalId, m => m.IsEqualTo(id))
            .And.Member(x => x.CheckoutUrl, m => m.IsEqualTo(url))
            .And.Member(x => x.ExpiresAtUtc, m => m.IsEqualTo(time));
    }

    [Test]
    public async Task Create_WithNullId_ShouldThrowArgumentNullException()
    {
        var action = () => ExternalSession.Create(null!, "url", DateTime.UtcNow);

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task Create_WithNullUrl_ShouldThrowArgumentNullException()
    {
        var action = () => ExternalSession.Create("id", null!, DateTime.UtcNow);

        await Assert.That(action).ThrowsExactly<ArgumentNullException>();
    }

    [Test]
    public async Task Create_WithWhiteId_ShouldThrowArgumentException()
    {
        var action = () => ExternalSession.Create("", "url", DateTime.UtcNow);

        await Assert.That(action).ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task Create_WithWhiteUrl_ShouldThrowArgumentException()
    {
        var action = () => ExternalSession.Create("id", "", DateTime.UtcNow);

        await Assert.That(action).ThrowsExactly<ArgumentException>();
    }

    [Test]
    public async Task Create_WithAlreadyExpiredTime_ShouldReturnSuccess()
    {
        var id = "id";
        var url = "url";
        var time = DateTime.UtcNow.AddMinutes(-10);

        var r = ExternalSession.Create(id, url, time);
        await Assert.That(r.IsSuccess).IsTrue();
    }

    [Test]
    public async Task Create_WithNotUtcExpireTime_ShouldReturnFailureWithExpirationTimeWasNotUtceError()
    {
        var id = "id";
        var url = "url";
        var timeNotUtc = new DateTime(2026, 5, 7, 10, 0, 0, DateTimeKind.Local); ;

        var r = ExternalSession.Create(id, url, timeNotUtc);

        await Assert.That(r.IsFailure).IsTrue();
        await Assert.That(r.Error.Code).IsEqualTo(ExternalSessionErrors.ExpirationTimeWasNotUtcCode);
    }

    [Test]
    public async Task Create_WithSameParameters_ShouldReturnEqualButDifferentInstances()
    {
        var time = DateTime.UtcNow;

        var session1 = ExternalSession.Create("id", "url", time).Value;
        var session2 = ExternalSession.Create("id", "url", time).Value;

        await Assert.That(session1).IsEqualTo(session2);
        await Assert.That(ReferenceEquals(session1, session2)).IsFalse();
    }

    [Test]
    public async Task Create_EqualObjects_ShouldHaveSameHashCode()
    {
        var time = DateTime.UtcNow;

        var session1 = ExternalSession.Create("id", "url", time).Value;
        var session2 = ExternalSession.Create("id", "url", time).Value;

        await Assert.That(session1.GetHashCode()).IsEqualTo(session2.GetHashCode());
    }
}
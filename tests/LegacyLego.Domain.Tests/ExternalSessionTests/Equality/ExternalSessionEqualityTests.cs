namespace LegacyLego.Domain.Tests.ExternalSessionTests;

public record ExternalSessionWrongEqualityTestCase(ExternalSession Session1, ExternalSession Session2);

public static class SessionTestData
{
    public static IEnumerable<TestDataRow<ExternalSessionWrongEqualityTestCase>> GetWrongComparisonCases()
    {
        var time = DateTime.UtcNow;

        yield return new(
            new ExternalSessionWrongEqualityTestCase(
                ExternalSession.Create("id1", "url", time).Value,
                ExternalSession.Create("id2", "url", time).Value
                ),
            DisplayName: "Sessions with different Id"
        );

        yield return new(
            new ExternalSessionWrongEqualityTestCase(
                ExternalSession.Create("id", "url1", time).Value,
                ExternalSession.Create("id", "url2", time).Value
                ),
            DisplayName: "Sessions with different Url"
        );

        yield return new(
            new ExternalSessionWrongEqualityTestCase(
                ExternalSession.Create("id", "url", DateTime.UtcNow.AddMinutes(10)).Value,
                ExternalSession.Create("id", "url", DateTime.UtcNow.AddMinutes(20)).Value
                ),
            DisplayName: "Sessions with different ExpiresAtUtc"
        );
    }
}

public class ExternalSessionEqualityTests
{
    [Test]
    public async Task Equals_WithSameValues_ShouldBeTrue()
    {
        var time = DateTime.UtcNow;

        var s1 = ExternalSession.Create("id","url", time).Value;
        var s2 = ExternalSession.Create("id", "url", time).Value;

        await Assert.That(s1).IsEqualTo(s2);
    }

    [Test]
    [MethodDataSource(typeof(SessionTestData), nameof(SessionTestData.GetWrongComparisonCases))]
    public async Task Equals_WithDifferentValues_ShouldBeFalse(ExternalSessionWrongEqualityTestCase testCase)
    {
        await Assert.That(testCase.Session1).IsNotEqualTo(testCase.Session2);
    }

    [Test]
    public async Task EqualsOperator_WitSameValues_ShouldBeTrue()
    {
        var time = DateTime.UtcNow;

        var s1 = ExternalSession.Create("id", "url", time).Value;
        var s2 = ExternalSession.Create("id", "url", time).Value;

        await Assert.That(s1 == s2).IsTrue();
    }

    [Test]
    public async Task EqualsOperator_WithDifferentValues_ShouldBeFalse()
    {
        var time1 = DateTime.UtcNow.AddMinutes(10);
        var time2 = DateTime.UtcNow.AddMinutes(20);

        var s1 = ExternalSession.Create("id1", "url1", time1).Value;
        var s2 = ExternalSession.Create("id2", "url2", time2).Value;

        await Assert.That(s1 == s2).IsFalse();
    }

    [Test]
    public async Task NotEqualsOperator_WitSameValues_ShouldBeFalse()
    {
        var time = DateTime.UtcNow;

        var s1 = ExternalSession.Create("id", "url", time).Value;
        var s2 = ExternalSession.Create("id", "url", time).Value;

        await Assert.That(s1 != s2).IsFalse();
    }

    [Test]
    public async Task NotEqualsOperator_WithDifferentValues_ShouldBeTrue()
    {
        var time1 = DateTime.UtcNow.AddMinutes(10);
        var time2 = DateTime.UtcNow.AddMinutes(20);

        var s1 = ExternalSession.Create("id1", "url1", time1).Value;
        var s2 = ExternalSession.Create("id2", "url2", time2).Value;

        await Assert.That(s1 != s2).IsTrue();
    }

    [Test]
    public async Task GetHashCode_ForEqualObjects_ShouldBeSame()
    {
        var time = DateTime.UtcNow;

        var s1 = ExternalSession.Create("id", "url", time).Value;
        var s2 = ExternalSession.Create("id", "url", time).Value;

        await Assert.That(s1.GetHashCode()).IsEqualTo(s2.GetHashCode());
    }

    [Test]
    public async Task GetHashCode_ForDifferentObjects_ShouldBeDifferent()
    {
        var time1 = DateTime.UtcNow.AddMinutes(10);
        var time2 = DateTime.UtcNow.AddMinutes(20);

        var s1 = ExternalSession.Create("id1", "url1", time1).Value;
        var s2 = ExternalSession.Create("id2", "url2", time2).Value;

        await Assert.That(s1.GetHashCode()).IsNotEqualTo(s2.GetHashCode());
    }

    [Test]
    public async Task Create_WithSameParameters_ShouldReturnDifferentInstances()
    {
        var time = DateTime.UtcNow;

        var s1 = ExternalSession.Create("id", "url", time).Value;
        var s2 = ExternalSession.Create("id", "url", time).Value;

        await Assert.That(ReferenceEquals(s1, s2)).IsFalse();
    }

    [Test]
    public async Task Equals_WithNull_ShouldBeFalse()
    {
        var s = ExternalSession.Create("id", "url", DateTime.UtcNow).Value;

        await Assert.That(s.Equals(null)).IsFalse();
    }

    [Test]
    public async Task EqualsOperator_WithNull_ShouldBeFalse()
    {
        var s = ExternalSession.Create("id", "url", DateTime.UtcNow).Value;

        await Assert.That(s == null).IsFalse();
    }

    [Test]
    public async Task NotEqualsOperator_WithNull_ShouldBeTrue()
    {
        var s = ExternalSession.Create("id", "url", DateTime.UtcNow).Value;

        await Assert.That(s != null).IsTrue();
    }

    [Test]
    public async Task Equals_WithDifferentType_ShouldBeFalse()
    {
        var s = ExternalSession.Create("id", "url", DateTime.UtcNow).Value;

        await Assert.That(s.Equals("not a session")).IsFalse();
    }
}
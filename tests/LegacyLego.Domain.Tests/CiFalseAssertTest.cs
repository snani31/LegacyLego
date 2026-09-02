namespace LegacyLego.Domain.Tests;

public class CiFalseAssertTest
{
    [Test]
    public async Task ShouldReturnFalseAssert()
    {
        await Assert.That(Boolean.TrueString).IsEqualTo(Boolean.FalseString);
    }
}
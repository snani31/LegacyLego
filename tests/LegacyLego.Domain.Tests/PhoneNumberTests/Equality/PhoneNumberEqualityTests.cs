using System;
using System.Collections.Generic;
using System.Text;

namespace LegacyLego.Domain.Tests.PhoneNumberTests.Equality;

public class PhoneNumberEqualityTests
{
    [Test]
    public async Task Equals_WithSameNormalizedValue_ShouldBeTrue()
    {
        var phone1 = PhoneNumber.Create("+7 (999) 123-45-67").Value;
        var phone2 = PhoneNumber.Create("+79991234567").Value;

        await Assert.That(phone1).IsEqualTo(phone2);
        await Assert.That(phone1 == phone2).IsTrue();
        await Assert.That(phone1 != phone2).IsFalse();
    }

    [Test]
    public async Task Equals_WithDifferentValues_ShouldBeFalse()
    {
        var phone1 = PhoneNumber.Create("+79991234567").Value;
        var phone2 = PhoneNumber.Create("+79991234568").Value;

        await Assert.That(phone1).IsNotEqualTo(phone2);
        await Assert.That(phone1 == phone2).IsFalse();
        await Assert.That(phone1 != phone2).IsTrue();
    }

    [Test]
    public async Task GetHashCode_ForEqualPhoneNumbers_ShouldBeSame()
    {
        var phone1 = PhoneNumber.Create("+7 (999) 123-45-67").Value;
        var phone2 = PhoneNumber.Create("+79991234567").Value;

        await Assert.That(phone1.GetHashCode()).IsEqualTo(phone2.GetHashCode());
    }

    [Test]
    public async Task ToString_ShouldReturnValue()
    {
        var phone = PhoneNumber.Create("+7 (999) 123-45-67").Value;

        await Assert.That(phone.ToString()).IsEqualTo("+79991234567");
    }
}

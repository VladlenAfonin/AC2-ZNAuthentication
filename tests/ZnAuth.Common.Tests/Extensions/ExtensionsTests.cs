using System.Numerics;
using Xunit;
using ZnAuth.Common.Extensions;

namespace ZnAuth.Common.Tests.Extensions;

public class BigIntegerExtensionsTests
{
    [Fact]
    public void IsPrime_NumberIsPrime_ReturnsTrue()
    {
        // Mersenne prime number (2.5e968).
        var number = BigInteger.Pow(2, 3217) - 1;

        var result = number.IsPrime();

        Assert.True(result);
    }

    [Fact]
    public void IsPrime_NumberIsNotPrime_ReturnsFalse()
    {
        // The number is even.
        var number = BigInteger.Pow(2, 3217);

        var result = number.IsPrime();

        Assert.False(result);
    }
}


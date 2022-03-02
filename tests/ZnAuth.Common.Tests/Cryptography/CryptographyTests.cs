using System.Numerics;
using Xunit;
using ZnAuth.Common.Cryptography;
using ZnAuth.Common.Extensions;

namespace ZnAuth.Common.Tests;

public class BigIntegerGeneratorTests
{
    [Fact]
    public void GeneratePrimeBigInteger_WhenCalled_GeneratesPrimeNumber()
    {
        var prime = BigIntegerGenerator.GeneratePrimeBigInteger(128);

        var result = prime.IsPrime();

        Assert.True(result);
    }

    [Fact]
    public void GenerateTwoCoprimePrimeNumbers_WhenCalled_GeneratesTwoCoprimePrimeNumbers()
    {
        var (x, y) = BigIntegerGenerator.GenerateTwoCoprimePrimeNumbers(128);

        Assert.True(x.IsPrime());
        Assert.True(y.IsPrime());
        Assert.True(BigInteger.GreatestCommonDivisor(x, y) == 1);
    }

    [Fact]
    public void GenerateNumberInRange_WhenCalled_GeneratesNumeberInRange()
    {
        var number = BigIntegerGenerator.GenerateNumberInRange(100, 150);

        Assert.InRange(number, 100, 150);
    }
}

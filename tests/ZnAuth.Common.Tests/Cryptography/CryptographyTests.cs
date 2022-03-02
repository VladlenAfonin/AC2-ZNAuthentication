using System.Numerics;
using Xunit;
using ZnAuth.Common.Cryptography;
using ZnAuth.Common.Extensions;

namespace ZnAuth.Common.Tests;

public class BigIntegerGeneratorTests
{
    [Fact]
    public void GetPrime_WhenCalled_ReturnsPrimeNumber()
    {
        var prime = BigIntegerGenerator.GetPrime(128);

        var result = prime.IsPrime();

        Assert.True(result);
    }

    [Fact]
    public void GetTwoCoprimePrimeNumbers_WhenCalled_ReturnsTwoCoprimePrimeNumbers()
    {
        var (x, y) = BigIntegerGenerator.GetTwoCoprimePrimeNumbers(128);

        Assert.True(x.IsPrime());
        Assert.True(y.IsPrime());
        Assert.True(BigInteger.GreatestCommonDivisor(x, y) == 1);
    }

    [Fact]
    public void GetInRange_WhenCalled_ReturnsNumeberInRange()
    {
        BigInteger minimum = 100;
        BigInteger maximum = 150;

        var number = BigIntegerGenerator.GetInRange(minimum, maximum);

        Assert.InRange(number, minimum, maximum);
    }

    [Fact]
    public void GetCoprimeToInRange_WhenCalled_ReturnsCoprimeToInRange()
    {
        BigInteger minimum = 100000;
        BigInteger maximum = 10000000;
        var number = BigIntegerGenerator.GetPositive(128);

        var result = BigIntegerGenerator.GetCoprimeToInRange(
            number, minimum, maximum);

        Assert.InRange(result, minimum, maximum);
        Assert.True(BigInteger.GreatestCommonDivisor(result, number) == 1);
    }
}

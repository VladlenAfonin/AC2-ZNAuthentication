using Xunit;
using ZnAuth.Common.Cryptography;
using ZnAuth.Common.Extensions;

namespace ZnAuth.Common.Tests;

public class BigIntegerGeneratorTests
{
    [Fact]
    public void NewPrimeBigInteger_WhenCalled_GeneratesPrimeNumber()
    {
        var prime = BigIntegerGenerator.NewPrimeBigInteger(128);

        var result = prime.IsPrime();

        Assert.True(result);
    }
}

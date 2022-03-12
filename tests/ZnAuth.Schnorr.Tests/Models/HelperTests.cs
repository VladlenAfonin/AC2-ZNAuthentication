using System.Numerics;
using Xunit;
using ZnAuth.Common.Extensions;
using ZnAuth.Schnorr.Models;

namespace ZnAuth.Schnorr.Tests.Models;

public class HelperTests
{
    [Fact]
    public void GetParameters_WhenCalled_GeneratesCorrectParameters()
    {
        var (p, q, g) = Helper.GetParameters();

        Assert.True(p.IsPrime());
        Assert.True(q.IsPrime());
        Assert.Equal(0, (p - 1) % q);
        Assert.Equal(1, BigInteger.ModPow(g, q, p));
    }
}


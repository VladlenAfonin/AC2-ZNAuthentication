using System.Numerics;
using ZnAuth.Common.Cryptography;
using ZnAuth.Common.Extensions;

namespace ZnAuth.Schnorr.Models;

/// <summary>Helper-methods for Schnorr protocol.</summary>
public static class Helper
{
    /// <summary>Generate parameters for Schnorr protocol.</summary>
    /// <param name="pLengthBytes">Length of p.</param>
    /// <param name="qLengthBytes">Length of q.</param>
    /// <returns>The three parameters generated.</returns>
    public static (BigInteger p, BigInteger q, BigInteger g) GetParameters(
        int pLengthBytes = 128, // 128 bytes = 1024 bits
        int qLengthBytes = 20) // 20 bytes = 160 bits
    {
        var q = BigIntegerGenerator.GetPrime(qLengthBytes); 
        var p = BigIntegerGenerator
            .GetPositive(pLengthBytes - qLengthBytes) * q + 1; 

        // Continue generating until pair is found.
        while (!p.IsPrime())
        {
            q = BigIntegerGenerator.GetPrime(qLengthBytes);
            p = BigIntegerGenerator
                .GetPositive(pLengthBytes - qLengthBytes) * q + 1;
        }

        // Use Fermat's little theorem to generate g.
        var g = BigInteger.ModPow(
            value: BigIntegerGenerator.GetCoprimeTo(p, pLengthBytes),
            exponent: (p - 1) / q,
            modulus: p);

        return (p, q, g);
    }
}


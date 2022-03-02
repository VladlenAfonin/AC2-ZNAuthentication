using System.Numerics;
using ZnAuth.Common.Cryptography;

namespace ZnAuth.Common.Extensions;

/// <summary><see cref="BigInteger"/> extensions.</summary>
public static class BigIntegerExtensions
{
    /// <summary>
    /// Check if this big integer is prime using Miller-Rabin algorithm.
    /// </summary>
    /// <param name="value"><see cref="BigInteger"/> to check.</param>
    /// <param name="numberOfWitnesses">Number of witnesses to use.</param>
    /// <returns>True if the number is prime. False otherwise.</returns>
    public static bool IsPrime(
        this BigInteger value,
        int numberOfWitnesses = 10)
    {
        var d = value - 1;
        int s = 0;

        while (d % 2 == 0)
        {
            d /= 2;
            s += 1;
        }

        BigInteger a;

        for (int i = 0; i < numberOfWitnesses; i++)
        {
            do
            {
                a = BigIntegerGenerator.NewPositiveBigInteger(
                    value.ToByteArray().Length);
            }
            while (a < 2 || a >= value - 2);

            BigInteger x = BigInteger.ModPow(a, d, value);

            if (x == 1 || x == value - 1)
                continue;

            for (int r = 1; r < s; r++)
            {
                x = BigInteger.ModPow(x, 2, value);

                if (x == 1)
                    return false;

                if (x == value - 1)
                    break;
            }

            if (x != value - 1)
                return false;
        }

        return true;
    }
}

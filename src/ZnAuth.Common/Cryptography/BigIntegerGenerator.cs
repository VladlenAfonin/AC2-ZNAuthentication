using System.Numerics;
using System.Security.Cryptography;
using ZnAuth.Common.Extensions;

namespace ZnAuth.Common.Cryptography;

/// <summary>Used to generate new <see cref="BigInteger"/>s.</summary>
public static class BigIntegerGenerator
{
	/// <summary>Get new positive <see cref="BigInteger"/>.</summary>
    /// <param name="bytes">Number of bytes.</param>
    /// <returns>New positive random <see cref="BigInteger"/>.</returns>
	public static BigInteger GeneratePositiveBigInteger(int bytes) =>
		BigInteger.Abs(new BigInteger(RandomNumberGenerator.GetBytes(bytes)));

	/// <summary>
    /// Generates random prime <see cref="BigInteger"/> with given number of
    /// bytes.
    /// </summary>
    /// <param name="bytes">Number of bytes.</param>
    /// <returns>Random prime <see cref="BigInteger"/>.</returns>
    /// <remarks>
    /// The method is quite slow. Use for no more than 128-256 bytes. If needed,
    /// the size is arbitrary though.
    /// </remarks>
	public static BigInteger GeneratePrimeBigInteger(int bytes)
    {
		BigInteger result = GeneratePositiveBigInteger(bytes);

		while (!result.IsPrime())
			result = GeneratePositiveBigInteger(bytes);

		return result;
    }

    public static (BigInteger, BigInteger) GenerateTwoCoprimePrimeNumbers(
        int bytes)
    {
        var x = GeneratePrimeBigInteger(128);
        var y = GeneratePrimeBigInteger(128);

        if (BigInteger.GreatestCommonDivisor(x, y) != 1)
            y = GeneratePrimeBigInteger(128);

        return (x, y);
    }
}


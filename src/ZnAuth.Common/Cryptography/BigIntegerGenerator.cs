using System.Numerics;
using System.Security.Cryptography;
using ZnAuth.Common.Extensions;

namespace ZnAuth.Common.Cryptography;

/// <summary>Used to generate new <see cref="BigInteger"/>s.</summary>
public static class BigIntegerGenerator
{
	/// <summary>Get new positive <see cref="BigInteger"/>.</summary>
    /// <param name="bits">Number of bits.</param>
    /// <returns>New positive random <see cref="BigInteger"/>.</returns>
	public static BigInteger NewPositiveBigInteger(int bits) =>
		BigInteger.Abs(new BigInteger(RandomNumberGenerator.GetBytes(bits)));

	/// <summary>
    /// Generates random prime <see cref="BigInteger"/> with given number of
    /// bits.
    /// </summary>
    /// <param name="bits">Number of bits.</param>
    /// <returns>Random prime <see cref="BigInteger"/>.</returns>
    /// <remarks>
    /// The method is quite slow. Use for no more than 128 bits.
    /// </remarks>
	public static BigInteger NewPrimeBigInteger(int bits)
    {
		BigInteger result = NewPositiveBigInteger(bits);

		while (!result.IsPrime())
			result = NewPositiveBigInteger(bits);

		return result;
    }
}


using System.Numerics;
using System.Security.Cryptography;
using ZnAuth.Common.Extensions;

namespace ZnAuth.Common.Cryptography;

/// <summary>Used to generate new <see cref="BigInteger"/>s.</summary>
public static class BigIntegerGenerator
{
	/// <summary>Get new positive <see cref="BigInteger"/>.</summary>
    /// <param name="numberOfBytes">Number of bytes.</param>
    /// <returns>New positive random <see cref="BigInteger"/>.</returns>
	public static BigInteger GeneratePositiveBigInteger(int numberOfBytes) =>
		BigInteger.Abs(new BigInteger(RandomNumberGenerator.GetBytes(
            numberOfBytes)));

	/// <summary>
    /// Generates random prime <see cref="BigInteger"/> with given number of
    /// bytes.
    /// </summary>
    /// <param name="numberOfBytes">Number of bytes.</param>
    /// <returns>Random prime <see cref="BigInteger"/>.</returns>
    /// <remarks>
    /// The method is quite slow. Use for no more than 128-256 bytes. If needed,
    /// the size is arbitrary though.
    /// </remarks>
	public static BigInteger GeneratePrimeBigInteger(int numberOfBytes)
    {
		var result = GeneratePositiveBigInteger(numberOfBytes);

		while (!result.IsPrime())
			result = GeneratePositiveBigInteger(numberOfBytes);

		return result;
    }

    /// <summary>Generate two coprime prime numbers.</summary>
    /// <param name="numberOfBytes">Number of bytes in each number.</param>
    /// <returns>Tuple with two generated numbers.</returns>
    public static (BigInteger, BigInteger) GenerateTwoCoprimePrimeNumbers(
        int numberOfBytes)
    {
        var x = GeneratePrimeBigInteger(128);
        var y = GeneratePrimeBigInteger(128);

        if (BigInteger.GreatestCommonDivisor(x, y) != 1)
            y = GeneratePrimeBigInteger(128);

        return (x, y);
    }

    /// <summary>
    /// Generates a random <see cref="BigInteger"/> in given boundaries.
    /// </summary>
    /// <param name="minimum">Lower boundary.</param>
    /// <param name="maximum">Upper boundary.</param>
    /// <returns>Generated <see cref="BigInteger"/>.</returns>
    /// <remarks>
    /// This method is not tested for its cryptographic properties.
    /// </remarks>
    public static BigInteger GenerateNumberInRange(
        BigInteger minimum,
        BigInteger maximum)
    {
        var numberOfBytes = ((minimum + maximum) / 2).ToByteArray().Length;
        var number = GeneratePositiveBigInteger(numberOfBytes);

        while (number < minimum || number > maximum)
            number = GeneratePositiveBigInteger(numberOfBytes);

        return number;
    }
}


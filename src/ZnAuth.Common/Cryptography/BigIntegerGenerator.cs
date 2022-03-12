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
	public static BigInteger GetPositive(int numberOfBytes) =>
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
	public static BigInteger GetPrime(int numberOfBytes)
    {
		var result = GetPositive(numberOfBytes);

		while (!result.IsPrime())
			result = GetPositive(numberOfBytes);

		return result;
    }

    /// <summary>Generate number coprime to given one.</summary>
    /// <param name="number">Number generated one will be coprime to.</param>
    /// <param name="numberOfBytes">Length in number of bytes.</param>
    /// <returns>Number coprime to given one.</returns>
    public static BigInteger GetCoprimeTo(BigInteger number, int numberOfBytes)
    {
        var result = GetPositive(numberOfBytes);

        while (BigInteger.GreatestCommonDivisor(result, number) != 1)
            result = GetPositive(numberOfBytes);

        return result;
    }

    /// <summary>Generate two coprime prime numbers.</summary>
    /// <param name="numberOfBytes">Number of bytes in each number.</param>
    /// <returns>Tuple with two generated numbers.</returns>
    public static (BigInteger, BigInteger) GetTwoCoprimePrimeNumbers(
        int numberOfBytes)
    {
        var x = GetPrime(numberOfBytes);
        var y = GetPrime(numberOfBytes);

        while (BigInteger.GreatestCommonDivisor(x, y) != 1)
            y = GetPrime(numberOfBytes);

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
    public static BigInteger GetInRange(
        BigInteger minimum,
        BigInteger maximum)
    {
        var numberOfBytes = ((minimum + maximum) / 2).ToByteArray().Length;
        var number = GetPositive(numberOfBytes);

        while (number < minimum || number > maximum)
            number = GetPositive(numberOfBytes);

        return number;
    }

    /// <summary>
    /// Get number coprime to <paramref name="number"/> in the range given.
    /// </summary>
    /// <param name="number">
    /// Number that should be coprime to generated one.
    /// </param>
    /// <param name="minimum">Lower bound.</param>
    /// <param name="maximum">Upper bound.</param>
    /// <returns></returns>
    public static BigInteger GetCoprimeToInRange(
        BigInteger number,
        BigInteger minimum,
        BigInteger maximum)
    {
        var result = GetInRange(minimum, maximum);

        while (BigInteger.GreatestCommonDivisor(result, number) != 1)
            result = GetInRange(minimum, maximum);

        return result;
    }
}

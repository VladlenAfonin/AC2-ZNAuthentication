using ZnAuth.Common.Cryptography;
using System.Numerics;

var (x, y) = BigIntegerGenerator.GenerateTwoCoprimePrimeNumbers(128);

Console.WriteLine($"x:\n{x:x}");
Console.WriteLine($"y:\n{y:x}");
Console.WriteLine($"GCD(x, y):\n{BigInteger.GreatestCommonDivisor(x, y):x}");
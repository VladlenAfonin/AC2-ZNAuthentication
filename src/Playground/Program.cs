using ZnAuth.Common.Extensions;
using ZnAuth.Common.Cryptography;

var prime = BigIntegerGenerator.NewPrimeBigInteger(128);

Console.WriteLine($"{prime:x}");
Console.WriteLine($"{prime.IsPrime()}");
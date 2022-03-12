using System.Numerics;
using ZnAuth.Common.Cryptography;

namespace ZnAuth.Schnorr.Models;

public class Prover
{
    public BigInteger V { get; set; }

	private readonly BigInteger _a;

	public Prover(BigInteger p, BigInteger q, BigInteger g)
	{
		_a = BigIntegerGenerator.GetInRange(1, q - 1);
		V = BigInteger.Abs((g - _a) % p);
	}
}

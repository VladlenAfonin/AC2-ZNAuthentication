using System.Numerics;
using ZnAuth.Common.Cryptography;

namespace ZnAuth.Schnorr.Models;

public class Prover
{
	/// <summary>Prover's public key.</summary>
    public BigInteger v { get; set; }

	/// <summary>Prover's current public nonce.</summary>
    public BigInteger x { get; set; }

	/// <summary>Prover's current secret nonce.</summary>
	public BigInteger r;

	public BigInteger a;

	private readonly (BigInteger p, BigInteger q, BigInteger g)
        _protocolParameters;

	public Prover(BigInteger p, BigInteger q, BigInteger g)
	{
		_protocolParameters = (p, q, g);

		a = BigIntegerGenerator.GetInRange(1, q - 1);
		v = BigInteger.ModPow(g, q - a, p);
		// PublicKey = BigInteger.Abs((g - _secretKey) % p);
	}

	public BigInteger GeneratePublicNonce()
    {
		r = BigIntegerGenerator.GetInRange(
			0, _protocolParameters.q);

		x = BigInteger.ModPow(
            value: _protocolParameters.g,
            exponent: r,
            modulus: _protocolParameters.p);

		return x;
    }

	public BigInteger GenerateRequest(BigInteger e) =>
		(r + a * e) % _protocolParameters.q;
}

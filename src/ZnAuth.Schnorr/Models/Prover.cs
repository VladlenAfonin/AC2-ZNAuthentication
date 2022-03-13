using System.Numerics;
using ZnAuth.Common.Cryptography;

namespace ZnAuth.Schnorr.Models;

public class Prover
{
	/// <summary>Prover's public key.</summary>
    public BigInteger PublicKey { get; set; }

	/// <summary>Prover's current public nonce.</summary>
    public BigInteger PublicNonce { get; set; }

	/// <summary>Prover's current secret nonce.</summary>
	private BigInteger _secretNonce;

	private readonly BigInteger _secretKey;

	private readonly (BigInteger p, BigInteger q, BigInteger g)
        _protocolParameters;

	public Prover(BigInteger p, BigInteger q, BigInteger g)
	{
		_protocolParameters = (p, q, g);

		_secretKey = BigIntegerGenerator.GetInRange(1, q - 1);
		PublicKey = BigInteger.ModPow(g, q - _secretKey, p);
		// PublicKey = BigInteger.Abs((g - _secretKey) % p);
	}

	public BigInteger GeneratePublicNonce()
    {
		_secretNonce = BigIntegerGenerator.GetInRange(
			0, _protocolParameters.q);

		PublicNonce = BigInteger.ModPow(
            value: _protocolParameters.g,
            exponent: _secretNonce,
            modulus: _protocolParameters.p);

		return PublicNonce;
    }

	public BigInteger GenerateRequest(BigInteger VerifiersNonce) =>
		(_secretNonce + _secretKey * VerifiersNonce) %
			_protocolParameters.q;
}

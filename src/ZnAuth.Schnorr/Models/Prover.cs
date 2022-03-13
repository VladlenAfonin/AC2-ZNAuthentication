using System.Numerics;
using ZnAuth.Common.Cryptography;

namespace ZnAuth.Schnorr.Models;

/// <summary>Prover model for Schnorr protocol.</summary>
public class Prover
{
	/// <summary>Prover's public key.</summary>
    public BigInteger V { get; set; }

	/// <summary>Prover's current public nonce.</summary>
    public BigInteger X { get; set; }

	/// <summary>Prover's current secret nonce.</summary>
	public BigInteger R;

	/// <summary>Prover's secret nonce.</summary>
	public BigInteger A;

	/// <summary>Protocol parameters.</summary>
	private readonly (BigInteger p, BigInteger q, BigInteger g)
        _protocolParameters;

	/// <summary>
	/// Initialize a new <see cref="Prover"/>.
	/// </summary>
	/// <param name="p">Protocol parameter p.</param>
	/// <param name="q">Protocol parameter q.</param>
	/// <param name="g">Protocol parameter g.</param>
	public Prover(BigInteger p, BigInteger q, BigInteger g)
	{
		_protocolParameters = (p, q, g);

		A = BigIntegerGenerator.GetInRange(1, q - 1);
		V = BigInteger.ModPow(g, q - A, p);
	}

	/// <summary>
    /// Generate secret and public nonces and returns public one.
    /// </summary>
    /// <returns>Public nonce.</returns>
	public BigInteger GenerateNonce()
    {
		R = BigIntegerGenerator.GetInRange(
			0, _protocolParameters.q);

		X = BigInteger.ModPow(
            value: _protocolParameters.g,
            exponent: R,
            modulus: _protocolParameters.p);

		return X;
    }

	/// <summary>Generate prover's request for authentication.</summary>
    /// <param name="e">Verifier's nonce.</param>
    /// <returns>Request.</returns>
	public BigInteger GenerateRequest(BigInteger e) =>
		(R + A * e) % _protocolParameters.q;
}

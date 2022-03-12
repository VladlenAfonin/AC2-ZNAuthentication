using System.Numerics;
using ZnAuth.Common.Cryptography;

namespace ZnAuth.FiatShamir.Models;

/// <summary>Fiat-Shamir prover model.</summary>
public class Prover
{
	/// <summary>Prover's public key.</summary>
	public BigInteger PublicKey { get; private set; }

	/// <summary>Modulus agreed upon.</summary>
	public BigInteger Modulus { get; }

	/// <summary>Current random bit.</summary>
	public int CurrentRandomBit { get; set; }

	/// <summary>Public nonce generated from the secret nonce.</summary>
	public BigInteger PublicNonce =>
		BigInteger.ModPow(_secretNonce, 2, Modulus);

	/// <summary>Generates a round proof.</summary>
	public BigInteger Proof => _secretNonce *
		BigInteger.Pow(_secretKey, CurrentRandomBit) % Modulus;

	/// <summary>Prover's secret round nonce.</summary>
	private BigInteger _secretNonce;

	/// <summary>Prover's secret key.</summary>
    private BigInteger _secretKey;

	/// <summary>Initialize a new <see cref="Prover"/> object.</summary>
    /// <param name="n">Modulus agreed upon.</param>
	public Prover(BigInteger n)
    {
		Modulus = n;

		GenerateSecretKey();
		GeneratePublicKey();
	}

	/// <summary>Generate secret nonce. Store the nonce generated.</summary>
    /// <returns>Public nonce based on secret one.</returns>
	public BigInteger GenerateNonce()
    {
		_secretNonce = BigIntegerGenerator.GetInRange(1, Modulus - 1);

		return PublicNonce;
    }

	/// <summary>Generate secret key.</summary>
	private void GenerateSecretKey() =>
        _secretKey = BigIntegerGenerator.GetCoprimeToInRange(
			Modulus, 1, Modulus - 1);

	/// <summary>Generate public key from the secret key.</summary>
	private void GeneratePublicKey() =>
		PublicKey = BigInteger.ModPow(_secretKey, 2, Modulus);
}

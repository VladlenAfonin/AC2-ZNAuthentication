using System.Numerics;
using System.Security.Cryptography;

namespace ZnAuth.FiatShamir.Models;

/// <summary>Fiat-Shamir verifier model.</summary>
public class Verifier
{
    /// <summary>Current random bit.</summary>
    public int CurrentRandomBit { get; private set; }

    /// <summary>Modulus agreed upon.</summary>
    public BigInteger Modulus { get; }

    /// <summary>Current prover's nonce.</summary>
    public BigInteger ProversNonce { get; set; }

    /// <summary>Current prover's public key.</summary>
    public BigInteger ProversPublicKey { get; set; }

    /// <summary>Initialize a new <see cref="Verifier"/>.</summary>
    /// <param name="n">Modulus agreed upon.</param>
    /// <param name="proversPublicKey">Prover's public key.</param>
    public Verifier(BigInteger n, BigInteger proversPublicKey)
    {
        Modulus = n;
        ProversPublicKey = proversPublicKey;
    }

    /// <summary>Verify prover's round final message.</summary>
    /// <param name="message">Round authentication message.</param>
    /// <returns>
    /// True if authentication is successful. False Otherwise.
    /// </returns>
    public bool Verify(BigInteger message) =>
        BigInteger.ModPow(message, 2, Modulus) == (ProversNonce *
            BigInteger.Pow(ProversPublicKey, CurrentRandomBit) % Modulus);

    /// <summary>Generate a random bit. Store the bit generated.</summary>
    /// <returns>Either 0 or 1.</returns>
    public int GenerateRandomBit()
    {
        CurrentRandomBit = RandomNumberGenerator.GetInt32(0, 2);

        return CurrentRandomBit;
    }
}

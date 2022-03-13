using System.Numerics;
using ZnAuth.Common.Cryptography;

namespace ZnAuth.Schnorr.Models;

/// <summary>Verifier model for Schnorr protocol.</summary>
public class Verifier
{
    /// <summary>Prover's public key.</summary>
    public BigInteger V { get; set; }

    /// <summary>Prover's public nonce.</summary>
	public BigInteger X { get; set; }

    /// <summary>Protocol's security parameter.</summary>
    public BigInteger SecurityParameter { get; set; }

    /// <summary>Verifier's nonce.</summary>
    public BigInteger E;

    /// <summary>Protocol's parameters.</summary>
    private readonly (BigInteger p, BigInteger q, BigInteger g)
        _protocolParameters;

    /// <summary>Initialize a new <see cref="Verifier"/>.</summary>
    /// <param name="p">Protocol parameter p.</param>
    /// <param name="q">Protocol parameter q.</param>
    /// <param name="g">Protocol parameter g.</param>
    /// <param name="securityParameter">Protocol security parameter.</param>
    public Verifier(
        BigInteger p,
        BigInteger q,
        BigInteger g,
        int securityParameter)
    {
        _protocolParameters = (p, q, g);

        SecurityParameter = securityParameter;
    }

    /// <summary>Generate verifier's nonce.</summary>
    /// <returns>Nonce generated.</returns>
    public BigInteger GenerateNonce()
    {
        E = BigIntegerGenerator.GetInRange(
            0, BigInteger.Pow(2, (int) SecurityParameter) + 1);

        return E;
    }

    /// <summary>Verify prover's request.</summary>
    /// <param name="y">Prover's request.</param>
    /// <returns>True if verification was successful.</returns>
    public bool Verify(BigInteger y) =>
        X ==
            BigInteger.ModPow(_protocolParameters.g, y, _protocolParameters.p) *
            BigInteger.ModPow(V, E, _protocolParameters.p) %
            _protocolParameters.p;
}

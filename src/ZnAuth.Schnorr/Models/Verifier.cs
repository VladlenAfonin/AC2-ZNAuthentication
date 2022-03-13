using System.Numerics;
using ZnAuth.Common.Cryptography;

namespace ZnAuth.Schnorr.Models;

public class Verifier
{
    public BigInteger ProversPublicKey { get; set; }

	public BigInteger ProversCurrentNonce { get; set; }

    public BigInteger SecurityParameter { get; set; }

    private BigInteger _currentNonce;

    private readonly (BigInteger p, BigInteger q, BigInteger g)
        _protocolParameters;

    public Verifier(
        BigInteger p,
        BigInteger q,
        BigInteger g,
        int securityParameter)
    {
        _protocolParameters = (p, q, g);

        SecurityParameter = securityParameter;
    }

    public BigInteger GenerateNonce()
    {
        _currentNonce = BigIntegerGenerator.GetInRange(
            0, BigInteger.Pow(2, (int) SecurityParameter) + 1);

        return _currentNonce;
    }

    public bool Verify(BigInteger request) =>
        ProversCurrentNonce ==
            BigInteger.ModPow(_protocolParameters.g, request, _protocolParameters.p) *
            BigInteger.ModPow(ProversPublicKey, _currentNonce, _protocolParameters.p) %
            _protocolParameters.p;
}

using System.Numerics;
using ZnAuth.Common.Cryptography;

namespace ZnAuth.Schnorr.Models;

public class Verifier
{
    public BigInteger v { get; set; }

	public BigInteger x { get; set; }

    public BigInteger SecurityParameter { get; set; }

    public BigInteger e;

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
        e = BigIntegerGenerator.GetInRange(
            0, BigInteger.Pow(2, (int) SecurityParameter) + 1);

        return e;
    }

    public bool Verify(BigInteger y) =>
        x ==
            BigInteger.ModPow(_protocolParameters.g, y, _protocolParameters.p) *
            BigInteger.ModPow(v, e, _protocolParameters.p) %
            _protocolParameters.p;
}

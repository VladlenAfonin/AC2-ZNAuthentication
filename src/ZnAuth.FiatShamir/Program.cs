using ZnAuth.Common.Cryptography;
using ZnAuth.FiatShamir.Models;

// Parameter generation
var numberOfRounds = 10;

var (p, q) = BigIntegerGenerator.GetTwoCoprimePrimeNumbers(128);
var n = p * q;

// Participants initialization.
var prover = new Prover(n);

// Enable for unsuccessful authentication result.
//prover.SecretKey = 5;

var verifier = new Verifier(n, prover.PublicKey);

for (int i = 0; i < numberOfRounds; i++)
{
    // 1. Prover sends `x = r^2 mod N` to the verifier.
    verifier.ProversNonce = prover.GenerateNonce();

    // 2. Verifier generates a random bit and sends it to prover.
    prover.CurrentRandomBit = verifier.GenerateRandomBit();

    // 3. Prover sends `y = r * s^e mod N` and
    // 4. Verfier checks `y^2 == x * v^2 mod N`.
    if (!verifier.Verify(prover.Proof))
    {
        Console.WriteLine("Authentication failed.");

        return;
    }
}

Console.WriteLine("Authentication succeded.");

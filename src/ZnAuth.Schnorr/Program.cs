using ZnAuth.Schnorr.Models;

var (p, q, g) = Helper.GetParameters(); // pLengthBytes: 4, qLengthBytes: 2);
var t = 10;

var prover = new Prover(p, q, g);

// Enable for unsuccessful authentication result.
// prover.a = 5;

var verifier = new Verifier(p, q, g, t);

verifier.x = prover.GeneratePublicNonce();
verifier.v = prover.v;

var e = verifier.GenerateNonce();

var y = prover.GenerateRequest(e);

var result = verifier.Verify(y);

Console.WriteLine($"Result: {result}.");
using ZnAuth.Schnorr.Models;

// Generate protocol parameters.
var (p, q, g) = Helper.GetParameters();
// Set security parameter.
var t = 72;

// Create prover.
var prover = new Prover(p, q, g);

// Enable for unsuccessful authentication result.
//prover.a = 5;

// Create verifier.
var verifier = new Verifier(p, q, g, t);

// Give prover's public key to verifier.
verifier.V = prover.V;

// Prover generates a public nonce.
verifier.X = prover.GenerateNonce();

// Verifier generates his nonce.
var e = verifier.GenerateNonce();

// Prover generates his request for verification.
var y = prover.GenerateRequest(e);

// Verifier verifies the request.
var result = verifier.Verify(y);

Console.WriteLine($"Result: {result}.");
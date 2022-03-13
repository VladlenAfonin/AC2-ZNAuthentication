using System.Numerics;
using ZnAuth.Common.Cryptography;
using ZnAuth.Common.Extensions;
using ZnAuth.Schnorr.Models;

var (p, q, g) = Helper.GetParameters(); // pLengthBytes: 4, qLengthBytes: 2);

var prover = new Prover(p, q, g);
var verifier = new Verifier(p, q, g, 10);

verifier.ProversCurrentNonce = prover.GeneratePublicNonce();

var verifiersNonce = verifier.GenerateNonce();

var request = prover.GenerateRequest(verifiersNonce);

var result = verifier.Verify(request);

Console.WriteLine($"Result: {result}.");
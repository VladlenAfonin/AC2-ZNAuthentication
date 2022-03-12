using System.Numerics;
using ZnAuth.Common.Cryptography;
using ZnAuth.Common.Extensions;
using ZnAuth.Schnorr.Models;

var (p, q, g) = Helper.GetParameters();

var prover = new Prover(p, q, g);

Console.WriteLine($"{prover.V:x}");

//Console.WriteLine($"{(5 - 16) % 5:x}");
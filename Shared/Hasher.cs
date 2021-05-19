using System;
using System.Linq;
using System.Security.Cryptography;

using Utility;

namespace Utility
{
  internal sealed class Hasher
  {
    private const int SaltSize = 16; // 128 bit
    private const int KeySize = 32; // 256 bit
    private const int Iterations = 10000;

    public string Encode(string text)
    {
      using var algorithm = new Rfc2898DeriveBytes(text, SaltSize, Iterations, HashAlgorithmName.SHA256);

      return Convert.ToBase64String(algorithm.GetBytes(KeySize)).DotAnd(Convert.ToBase64String(algorithm.Salt));
    }

    public bool Compare(string hash, string text)
    {
      var s = hash.Split(".");
      if (s.Length != 2)
        throw new NotSupportedException();

      using var algorithm = new Rfc2898DeriveBytes(text, Convert.FromBase64String(s[1]), Iterations, HashAlgorithmName.SHA256);

      return algorithm.GetBytes(KeySize).SequenceEqual(Convert.FromBase64String(s[0]));
    }
  }
}
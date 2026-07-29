using System.Security.Cryptography;

namespace Itmo.ObjectOrientedProgramming.Lab3.Services;

/// <summary>
/// Provides cryptographically secure random selection.
/// </summary>
public class SecureRandomSelector
{
    private readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();

    public int SelectIndex(int maxExclusive)
    {
        if (maxExclusive <= 0)
            throw new ArgumentException("Max must be positive", nameof(maxExclusive));

        byte[] bytes = new byte[4];
        _rng.GetBytes(bytes);
        int value = BitConverter.ToInt32(bytes, 0) & int.MaxValue;
        return value % maxExclusive;
    }
}

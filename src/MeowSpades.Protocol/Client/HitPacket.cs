using MeowSpades.Protocol.Enums;

namespace MeowSpades.Protocol.Client;

/// <summary>Reports a hit to the server.</summary>
/// <param name="TargetPlayerId">The player identifier that was hit.</param>
/// <param name="Type">The hit region or attack type.</param>
public readonly record struct HitPacket(byte TargetPlayerId, HitType Type) : IPacket<HitPacket>
{
    /// <inheritdoc />
    public static byte Id => 5;

    public static HitPacket Deserialize(ReadOnlySpan<byte> source)
    {
        throw new NotImplementedException();
    }

    public int GetMaxSize()
    {
        throw new NotImplementedException();
    }

    public int Serialize(Span<byte> destination)
    {
        throw new NotImplementedException();
    }
}

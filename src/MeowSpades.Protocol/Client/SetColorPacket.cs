namespace MeowSpades.Protocol.Client;

/// <summary>Sets the color of a player's held block.</summary>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="Blue">The blue color component.</param>
/// <param name="Green">The green color component.</param>
/// <param name="Red">The red color component.</param>
public readonly record struct SetColorPacket(
    byte PlayerId,
    byte Blue,
    byte Green,
    byte Red
) : IPacket<SetColorPacket>
{
    /// <inheritdoc />
    public static byte Id => 8;

    public static SetColorPacket Deserialize(ReadOnlySpan<byte> source)
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

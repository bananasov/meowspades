namespace MeowSpades.Protocol.Server;

/// <summary>Sets the client's fog color.</summary>
/// <param name="Alpha">The unused alpha component.</param>
/// <param name="Blue">The blue component.</param>
/// <param name="Green">The green component.</param>
/// <param name="Red">The red component.</param>
public readonly record struct FogColorPacket(byte Alpha, byte Blue, byte Green, byte Red) : IPacket<FogColorPacket>
{
    /// <inheritdoc />
    public static byte Id => 27;

    public int GetMaxSize()
    {
        throw new NotImplementedException();
    }

    public static FogColorPacket Deserialize(ReadOnlySpan<byte> source)
    {
        throw new NotImplementedException();
    }

    public int Serialize(Span<byte> destination)
    {
        throw new NotImplementedException();
    }
}

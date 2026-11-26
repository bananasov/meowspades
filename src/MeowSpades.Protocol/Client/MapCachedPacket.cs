namespace MeowSpades.Protocol.Client;

/// <summary>Reports whether the map is cached by the client.</summary>
/// <param name="Cached">Whether the map is cached locally.</param>
public readonly record struct MapCachedPacket(bool Cached) : IPacket<MapCachedPacket>
{
    /// <inheritdoc />
    public static byte Id => 31;

    public static MapCachedPacket Deserialize(ReadOnlySpan<byte> source)
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

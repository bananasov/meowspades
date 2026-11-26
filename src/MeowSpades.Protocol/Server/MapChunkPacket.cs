namespace MeowSpades.Protocol.Server;

/// <summary>Contains a chunk of compressed map data.</summary>
/// <param name="Data">The DEFLATE/zlib-encoded map data.</param>
public readonly record struct MapChunkPacket(ReadOnlyMemory<byte> Data) : IPacket<MapChunkPacket>
{
    /// <inheritdoc />
    public static byte Id => 19;

    public static MapChunkPacket Deserialize(ReadOnlySpan<byte> source)
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

namespace MeowSpades.Protocol.Server;

/// <summary>Contains a chunk of compressed map data.</summary>
/// <param name="Data">The DEFLATE/zlib-encoded map data.</param>
public readonly record struct MapChunkPacket(ReadOnlyMemory<byte> Data) : IPacket
{
    /// <inheritdoc />
    public static byte Id => 19;
}

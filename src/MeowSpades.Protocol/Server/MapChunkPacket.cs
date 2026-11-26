using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Server;

/// <summary>Contains a chunk of compressed map data.</summary>
/// <param name="Data">The DEFLATE/zlib-encoded map data.</param>
public readonly record struct MapChunkPacket(ReadOnlyMemory<byte> Data) : IPacket<MapChunkPacket>
{
    /// <inheritdoc />
    public static byte Id => 19;

    /// <inheritdoc />
    public static MapChunkPacket Deserialize(ReadOnlySpan<byte> source)
    {
        PacketValidation.ThrowIfTooShort(source, sizeof(byte));

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var data = reader.ReadBytes(reader.Remaining);

        return new MapChunkPacket(data.ToArray());
    }

    /// <inheritdoc />
    public int GetMaxSize() => 1 + Data.Length;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteBytes(Data.Span);

        return writer.WrittenCount;
    }
}

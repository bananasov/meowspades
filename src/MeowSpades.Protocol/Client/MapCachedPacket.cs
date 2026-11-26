using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Client;

/// <summary>Reports whether the map is cached by the client.</summary>
/// <param name="Cached">Whether the map is cached locally.</param>
public readonly record struct MapCachedPacket(bool Cached) : IPacket<MapCachedPacket>
{
    /// <inheritdoc />
    public static byte Id => 31;

    public static MapCachedPacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 2;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var cached = reader.ReadBoolean();
        return new MapCachedPacket(cached);
    }

    /// <inheritdoc />
    public int GetMaxSize() => 2;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteBoolean(Cached);

        return writer.WrittenCount;
    }
}

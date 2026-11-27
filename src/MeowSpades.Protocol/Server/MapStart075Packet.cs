using MeowSpades.Binary;
using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Server;

/// <summary>Begins a protocol 0.75 map transfer.</summary>
/// <param name="MapSize">The uncompressed map size in bytes.</param>
public readonly record struct MapStart075Packet(uint MapSize) : IPacket<MapStart075Packet>
{
    /// <inheritdoc />
    public static byte Id => 18;

    /// <inheritdoc />
    public static MapStart075Packet Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 5;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var mapSize = reader.ReadUInt32();

        return new MapStart075Packet(mapSize);
    }

    /// <inheritdoc />
    public int GetMaxSize() => 5;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteUInt32(MapSize);

        return writer.WrittenCount;
    }
}

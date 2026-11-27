using System.Text;
using MeowSpades.Binary;
using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Server;

/// <summary>Begins a protocol 0.76 map transfer.</summary>
/// <param name="MapSize">The uncompressed map size in bytes.</param>
/// <param name="Crc32">The map CRC-32 value.</param>
/// <param name="MapName">The CP437 map name as a .NET string.</param>
public readonly record struct MapStart076Packet(uint MapSize, uint Crc32, string MapName) : IPacket<MapStart076Packet>
{
    private const int FixedSize = 9;
    private static readonly Encoding Cp437 = Encoding.GetEncoding(437);

    /// <inheritdoc />
    public static byte Id => 18;

    /// <inheritdoc />
    public static MapStart076Packet Deserialize(ReadOnlySpan<byte> source)
    {
        PacketValidation.ThrowIfTooShort(source, FixedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var mapSize = reader.ReadUInt32();
        var crc32 = reader.ReadUInt32();

        var mapNameBytes = reader.ReadBytes(reader.Remaining);
        var mapName = Cp437.GetString(mapNameBytes);

        return new MapStart076Packet(mapSize, crc32, mapName);
    }

    /// <inheritdoc />
    public int GetMaxSize() => FixedSize + Cp437.GetByteCount(MapName);

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteUInt32(MapSize);
        writer.WriteUInt32(Crc32);
        writer.WriteBytes(Cp437.GetBytes(MapName));

        return writer.WrittenCount;
    }
}

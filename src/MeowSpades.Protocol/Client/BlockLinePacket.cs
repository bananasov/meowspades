using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Client;

/// <summary>Creates a line of blocks between two map coordinates.</summary>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="StartX">The starting X coordinate.</param>
/// <param name="StartY">The starting Y coordinate.</param>
/// <param name="StartZ">The starting Z coordinate.</param>
/// <param name="EndX">The ending X coordinate.</param>
/// <param name="EndY">The ending Y coordinate.</param>
/// <param name="EndZ">The ending Z coordinate.</param>
public readonly record struct BlockLinePacket(
    byte PlayerId,
    int StartX,
    int StartY,
    int StartZ,
    int EndX,
    int EndY,
    int EndZ
) : IPacket<BlockLinePacket>
{
    /// <inheritdoc />
    public static byte Id => 14;

    /// <inheritdoc />
    public int GetMaxSize() => 26;

    /// <inheritdoc />
    public static BlockLinePacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 26;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var playerId = reader.ReadByte();
        var startX = reader.ReadInt32();
        var startY = reader.ReadInt32();
        var startZ = reader.ReadInt32();

        var endX = reader.ReadInt32();
        var endY = reader.ReadInt32();
        var endZ = reader.ReadInt32();

        return new BlockLinePacket(playerId, startX, startY, startZ, endX, endY, endZ);
    }

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(PlayerId);

        writer.WriteInt32(StartX);
        writer.WriteInt32(StartY);
        writer.WriteInt32(StartZ);

        writer.WriteInt32(EndX);
        writer.WriteInt32(EndY);
        writer.WriteInt32(EndZ);

        return writer.WrittenCount;
    }
}

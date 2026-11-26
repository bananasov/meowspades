using MeowSpades.Protocol.Enums;
using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Client;

/// <summary>Places or destroys a block at integer map coordinates.</summary>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="Action">The block action.</param>
/// <param name="X">The X map coordinate.</param>
/// <param name="Y">The Y map coordinate.</param>
/// <param name="Z">The Z map coordinate.</param>
public readonly record struct BlockActionPacket(
    byte PlayerId,
    BlockActionType Action,
    int X,
    int Y,
    int Z
) : IPacket<BlockActionPacket>
{
    /// <inheritdoc />
    public static byte Id => 13;

    /// <inheritdoc />
    public int GetMaxSize() => 15;

    /// <inheritdoc />
    public static BlockActionPacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 15;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var playerId = reader.ReadByte();
        var actionType = (BlockActionType)reader.ReadByte();
        var x = reader.ReadInt32();
        var y = reader.ReadInt32();
        var z = reader.ReadInt32();

        return new BlockActionPacket(playerId, actionType, x, y, z);
    }

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(PlayerId);
        writer.WriteByte((byte)Action);
        writer.WriteInt32(X);
        writer.WriteInt32(Y);
        writer.WriteInt32(Z);

        return writer.WrittenCount;
    }
}

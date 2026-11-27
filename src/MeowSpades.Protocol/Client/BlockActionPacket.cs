using System.Numerics;
using MeowSpades.Protocol.Enums;
using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Client;

/// <summary>Places or destroys a block at integer map coordinates.</summary>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="Action">The block action.</param>
/// <param name="Location">The integer map coordinates of the block.</param>
public readonly record struct BlockActionPacket(
    byte PlayerId,
    BlockActionType Action,
    Vector3 Location
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
        var action = (BlockActionType)reader.ReadByte();
        var location = reader.ReadInt32Vector3();

        return new BlockActionPacket(playerId, action, location);
    }

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(PlayerId);
        writer.WriteByte((byte)Action);
        writer.WriteInt32Vector3(Location);

        return writer.WrittenCount;
    }
}

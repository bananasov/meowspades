using MeowSpades.Protocol.Enums;
using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Server;

/// <summary>Sets a player's equipped tool.</summary>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="Tool">The equipped tool.</param>
public readonly record struct SetToolPacket(byte PlayerId, ToolType Tool) : IPacket<SetToolPacket>
{
    /// <inheritdoc />
    public static byte Id => 7;

    /// <inheritdoc />
    public static SetToolPacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 3;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var playerId = reader.ReadByte();
        var tool = (ToolType)reader.ReadByte();

        return new SetToolPacket(playerId, tool);
    }

    /// <inheritdoc />
    public int GetMaxSize() => 3;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(PlayerId);
        writer.WriteByte((byte)Tool);

        return writer.WrittenCount;
    }
}

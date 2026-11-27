using MeowSpades.Binary;
using MeowSpades.Protocol.IO;
using MeowSpades.Protocol.Models;

namespace MeowSpades.Protocol.Client;

/// <summary>Sets the color of a player's held block.</summary>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="Color">The held block color.</param>
public readonly record struct SetColorPacket(
    byte PlayerId,
    Color Color
) : IPacket<SetColorPacket>
{
    /// <inheritdoc />
    public static byte Id => 8;

    /// <inheritdoc />
    public static SetColorPacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 5;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var playerId = reader.ReadByte();
        var color = reader.ReadColor();

        return new SetColorPacket(playerId, color);
    }

    /// <inheritdoc />
    public int GetMaxSize() => 5;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(PlayerId);
        writer.WriteColor(Color);

        return writer.WrittenCount;
    }
}

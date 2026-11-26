using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Client;

/// <summary>Sets the color of a player's held block.</summary>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="Blue">The blue color component.</param>
/// <param name="Green">The green color component.</param>
/// <param name="Red">The red color component.</param>
public readonly record struct SetColorPacket(
    byte PlayerId,
    byte Blue,
    byte Green,
    byte Red
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
        var blue = reader.ReadByte();
        var green = reader.ReadByte();
        var red = reader.ReadByte();

        return new SetColorPacket(playerId, blue, green, red);
    }

    /// <inheritdoc />
    public int GetMaxSize() => 5;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(PlayerId);
        writer.WriteByte(Blue);
        writer.WriteByte(Green);
        writer.WriteByte(Red);

        return writer.WrittenCount;
    }
}

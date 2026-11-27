using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Client;

/// <summary>Sets the sender's world position.</summary>
/// <param name="Position">The sender's world position.</param>
public readonly record struct PositionDataPacket(
    System.Numerics.Vector3 Position
) : IPacket<PositionDataPacket>
{
    /// <inheritdoc />
    public static byte Id => 0;

    /// <inheritdoc />
    public static PositionDataPacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 13;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var position = reader.ReadVector3();
        return new PositionDataPacket(position);
    }

    /// <inheritdoc />
    public int GetMaxSize() => 13;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteVector3(Position);

        return writer.WrittenCount;
    }
}

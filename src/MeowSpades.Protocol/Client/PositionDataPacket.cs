using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Client;

// TODO: Consider using Vector3 here, its the same anyways.
/// <summary>Sets the sender's world position.</summary>
/// <param name="X">The X coordinate.</param>
/// <param name="Y">The Y coordinate.</param>
/// <param name="Z">The Z coordinate.</param>
public readonly record struct PositionDataPacket(
    float X,
    float Y,
    float Z
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
        return new PositionDataPacket(position.X, position.Y, position.Z);
    }

    /// <inheritdoc />
    public int GetMaxSize() => 13;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteVector3(X, Y, Z);

        return writer.WrittenCount;
    }
}

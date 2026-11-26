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

        var x = reader.ReadFloat();
        var y = reader.ReadFloat();
        var z = reader.ReadFloat();

        return new PositionDataPacket(x, y, z);
    }

    /// <inheritdoc />
    public int GetMaxSize() => 13;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteFloat(X);
        writer.WriteFloat(Y);
        writer.WriteFloat(Z);

        return writer.WrittenCount;
    }
}

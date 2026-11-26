using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Client;

// TODO: Consider using Vector3 here, its the same anyways.
/// <summary>Sets the sender's world orientation.</summary>
/// <param name="X">The X component.</param>
/// <param name="Y">The Y component.</param>
/// <param name="Z">The Z component.</param>
public readonly record struct OrientationDataPacket(
    float X,
    float Y,
    float Z
) : IPacket<OrientationDataPacket>
{
    /// <inheritdoc />
    public static byte Id => 1;

    public static OrientationDataPacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 13;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        float x = reader.ReadFloat();
        float y = reader.ReadFloat();
        float z = reader.ReadFloat();

        return new OrientationDataPacket(x, y, z);
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

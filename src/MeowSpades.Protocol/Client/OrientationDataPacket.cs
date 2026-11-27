using MeowSpades.Binary;
using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Client;

/// <summary>Sets the sender's world orientation.</summary>
/// <param name="Orientation">The sender's world orientation vector.</param>
public readonly record struct OrientationDataPacket(
    System.Numerics.Vector3 Orientation
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

        var orientation = reader.ReadVector3();
        return new OrientationDataPacket(orientation);
    }

    /// <inheritdoc />
    public int GetMaxSize() => 13;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteVector3(Orientation);

        return writer.WrittenCount;
    }
}

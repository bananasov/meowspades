using MeowSpades.Binary;
using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Server;

/// <summary>Starts the extra-packet version handshake.</summary>
/// <param name="Challenge">The signed challenge value to return in the response.</param>
public readonly record struct VersionHandshakeInitPacket(int Challenge) : IPacket<VersionHandshakeInitPacket>
{
    /// <inheritdoc />
    public static byte Id => 31;

    /// <inheritdoc />
    public static VersionHandshakeInitPacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 5;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var challenge = reader.ReadInt32();
        return new VersionHandshakeInitPacket(challenge);
    }

    /// <inheritdoc />
    public int GetMaxSize() => 5;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteInt32(Challenge);

        return writer.WrittenCount;
    }
}

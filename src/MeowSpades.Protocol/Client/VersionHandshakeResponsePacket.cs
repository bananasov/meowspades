using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Client;

/// <summary>Returns the challenge from a version-handshake initialization.</summary>
/// <param name="Challenge">The challenge received from the server.</param>
public readonly record struct VersionHandshakeResponsePacket(
    int Challenge
) : IPacket<VersionHandshakeResponsePacket>
{
    /// <inheritdoc />
    public static byte Id => 32;

    /// <inheritdoc />
    public static VersionHandshakeResponsePacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 5;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var challenge = reader.ReadInt32();

        return new VersionHandshakeResponsePacket(challenge);
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

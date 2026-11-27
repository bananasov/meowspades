using MeowSpades.Binary;
using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Server;

/// <summary>Reports an intel capture event.</summary>
/// <param name="PlayerId">The capturing player's identifier.</param>
/// <param name="Winning">Whether the capture wins the game.</param>
public readonly record struct IntelCapturePacket(byte PlayerId, bool Winning) : IPacket<IntelCapturePacket>
{
    /// <inheritdoc />
    public static byte Id => 23;

    /// <inheritdoc />
    public int GetMaxSize() => 3;

    /// <inheritdoc />
    public static IntelCapturePacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 3;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var playerId = reader.ReadByte();
        var winning = reader.ReadBoolean();

        return new IntelCapturePacket(playerId, winning);
    }

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(PlayerId);
        writer.WriteBoolean(Winning);

        return writer.WrittenCount;
    }
}

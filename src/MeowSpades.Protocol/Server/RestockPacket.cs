using MeowSpades.Binary;
using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Server;

/// <summary>Reports that a player has been restocked.</summary>
/// <param name="PlayerId">The player's identifier.</param>
public readonly record struct RestockPacket(byte PlayerId) : IPacket<RestockPacket>
{
    /// <inheritdoc />
    public static byte Id => 26;

    /// <inheritdoc />
    public static RestockPacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 2;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var playerId = reader.ReadByte();

        return new RestockPacket(playerId);
    }

    /// <inheritdoc />
    public int GetMaxSize() => 2;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(PlayerId);

        return writer.WrittenCount;
    }
}

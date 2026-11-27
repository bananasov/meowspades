using MeowSpades.Binary;
using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Server;

/// <summary>Notifies clients that a player has disconnected.</summary>
/// <param name="PlayerId">The disconnected player's identifier.</param>
public readonly record struct PlayerLeftPacket(byte PlayerId) : IPacket<PlayerLeftPacket>
{
    /// <inheritdoc />
    public static byte Id => 20;

    /// <inheritdoc />
    public static PlayerLeftPacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 2;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var playerId = reader.ReadByte();

        return new PlayerLeftPacket(playerId);
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

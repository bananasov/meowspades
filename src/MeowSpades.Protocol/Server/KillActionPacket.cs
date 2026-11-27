using MeowSpades.Protocol.Enums;
using MeowSpades.Binary;
using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Server;

/// <summary>Notifies a client that a player has died.</summary>
/// <param name="PlayerId">The player that died.</param>
/// <param name="KillerId">The player who delivered the kill.</param>
/// <param name="Type">The cause of death.</param>
/// <param name="RespawnTime">Seconds until respawn.</param>
public readonly record struct KillActionPacket(
    byte PlayerId,
    byte KillerId,
    KillType Type,
    byte RespawnTime
) : IPacket<KillActionPacket>
{
    /// <inheritdoc />
    public static byte Id => 16;

    /// <inheritdoc />
    public static KillActionPacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 5;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var playerId = reader.ReadByte();
        var killerId = reader.ReadByte();
        var killType = (KillType)reader.ReadByte();
        var respawnTime = reader.ReadByte();

        return new KillActionPacket(playerId, killerId, killType, respawnTime);
    }

    /// <inheritdoc />
    public int GetMaxSize() => 5;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(PlayerId);
        writer.WriteByte(KillerId);
        writer.WriteByte((byte)Type);
        writer.WriteByte(RespawnTime);

        return writer.WrittenCount;
    }
}

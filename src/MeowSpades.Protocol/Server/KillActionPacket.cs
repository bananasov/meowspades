using MeowSpades.Protocol.Enums;

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

    public static KillActionPacket Deserialize(ReadOnlySpan<byte> source)
    {
        throw new NotImplementedException();
    }

    public int GetMaxSize()
    {
        throw new NotImplementedException();
    }

    public int Serialize(Span<byte> destination)
    {
        throw new NotImplementedException();
    }
}

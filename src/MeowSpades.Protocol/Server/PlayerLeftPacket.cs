namespace MeowSpades.Protocol.Server;

/// <summary>Notifies clients that a player has disconnected.</summary>
/// <param name="PlayerId">The disconnected player's identifier.</param>
public readonly record struct PlayerLeftPacket(byte PlayerId) : IPacket
{
    /// <inheritdoc />
    public static byte Id => 20;
}

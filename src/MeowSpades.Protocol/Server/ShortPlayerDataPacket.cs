namespace MeowSpades.Protocol.Server;

/// <summary>Announces a player's identifier, team, and weapon.</summary>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="Team">The team identifier.</param>
/// <param name="Weapon">The weapon identifier.</param>
public readonly record struct ShortPlayerDataPacket(byte PlayerId, byte Team, byte Weapon) : IPacket
{
    /// <inheritdoc />
    public static byte Id => 10;
}

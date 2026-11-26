namespace MeowSpades.Protocol.Client;

/// <summary>Announces a player's team, equipment, statistics, color, and name.</summary>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="Team">The team identifier.</param>
/// <param name="Weapon">The weapon identifier.</param>
/// <param name="HeldItem">The held item identifier.</param>
/// <param name="Kills">The kill count.</param>
/// <param name="Blue">The blue component.</param>
/// <param name="Green">The green component.</param>
/// <param name="Red">The red component.</param
/// ><param name="Name">The CP437-encoded name as a .NET string.</param>
public readonly record struct ExistingPlayerPacket(
    byte PlayerId,
    byte Team,
    byte Weapon,
    byte HeldItem,
    uint Kills,
    byte Blue,
    byte Green,
    byte Red,
    string Name
) : IPacket
{
    /// <inheritdoc />
    public static byte Id => 9;
}

using MeowSpades.Protocol;
using MeowSpades.Protocol.Enums;

namespace MeowSpades.Protocol.Client;

/// <summary>Updates a player's weapon button states.</summary>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="Buttons">The pressed weapon buttons.</param>
public readonly record struct WeaponInputPacket(byte PlayerId, WeaponButtonState Buttons) : IPacket
{
    /// <inheritdoc />
    public static byte Id => 4;
}

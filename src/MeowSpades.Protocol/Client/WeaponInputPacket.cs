using MeowSpades.Protocol;
using MeowSpades.Protocol.Enums;

namespace MeowSpades.Protocol.Client;

/// <summary>Updates a player's weapon button states.</summary>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="Buttons">The pressed weapon buttons.</param>
public readonly record struct WeaponInputPacket(byte PlayerId, WeaponButtonState Buttons) : IPacket<WeaponInputPacket>
{
    /// <inheritdoc />
    public static byte Id => 4;

    public static WeaponInputPacket Deserialize(ReadOnlySpan<byte> source)
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

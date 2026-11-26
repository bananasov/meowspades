namespace MeowSpades.Protocol.Client;

/// <summary>Requests a weapon change.</summary>
/// <param name="PlayerId">The player's identifier.</param>
/// <param name="WeaponId">The requested weapon identifier.</param>
public readonly record struct ChangeWeaponPacket(
    byte PlayerId,
    byte WeaponId
) : IPacket<ChangeWeaponPacket>
{
    /// <inheritdoc />
    public static byte Id => 30;

    public static ChangeWeaponPacket Deserialize(ReadOnlySpan<byte> source)
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

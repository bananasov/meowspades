namespace MeowSpades.Protocol.Server;

/// <summary>Reports a player's weapon reload.</summary>
/// <param name="PlayerId">The reloading player's identifier.</param>
/// <param name="ClipAmmo">The remaining clip ammunition.</param>
/// <param name="ReserveAmmo">The remaining reserve ammunition.</param>
public readonly record struct WeaponReloadPacket(
    byte PlayerId,
    byte ClipAmmo,
    byte ReserveAmmo
) : IPacket<WeaponReloadPacket>
{
    /// <inheritdoc />
    public static byte Id => 28;

    public static WeaponReloadPacket Deserialize(ReadOnlySpan<byte> source)
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

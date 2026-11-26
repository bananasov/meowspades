namespace MeowSpades.Protocol.Client;

/// <summary>Spawns a grenade with its fuse, position, and velocity.</summary>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="FuseLength">The fuse duration.</param>
/// <param name="X">The X coordinate.</param>
/// <param name="Y">The Y coordinate.</param>
/// <param name="Z">The Z coordinate.</param>
/// <param name="VelocityX">The X velocity.</param>
/// <param name="VelocityY">The Y velocity.</param>
/// <param name="VelocityZ">The Z velocity.</param>
public readonly record struct GrenadePacket(
    byte PlayerId,
    float FuseLength,
    float X,
    float Y,
    float Z,
    float VelocityX,
    float VelocityY,
    float VelocityZ
) : IPacket
{
    /// <inheritdoc />
    public static byte Id => 6;
}

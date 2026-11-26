namespace MeowSpades.Protocol.Server;

/// <summary>Reports that a player picked up the intel.</summary>
/// <param name="PlayerId">The player's identifier.</param>
public readonly record struct IntelPickupPacket(byte PlayerId) : IPacket
{
    /// <inheritdoc />
    public static byte Id => 24;
}

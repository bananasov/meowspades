namespace MeowSpades.Protocol.Server;

/// <summary>Reports a Territory Control capture event.</summary>
/// <param name="PlayerId">The capturing player's identifier.</param>
/// <param name="EntityId">The captured command post identifier.</param>
/// <param name="Winning">Whether the capture wins the game.</param>
/// <param name="State">The team identifier associated with the capture.</param>
public readonly record struct TerritoryCapturePacket(
    byte PlayerId,
    byte EntityId,
    bool Winning,
    byte State
) : IPacket
{
    /// <inheritdoc />
    public static byte Id => 21;
}

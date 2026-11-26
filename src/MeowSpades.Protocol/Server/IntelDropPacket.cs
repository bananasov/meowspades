namespace MeowSpades.Protocol.Server;

/// <summary>Reports that a player dropped intel.</summary>
/// <param name="PlayerId">The player's identifier.</param>
/// <param name="X">The drop X coordinate.</param>
/// <param name="Y">The drop Y coordinate.</param>
/// <param name="Z">The drop Z coordinate.</param>
public readonly record struct IntelDropPacket(byte PlayerId, float X, float Y, float Z) : IPacket
{
    /// <inheritdoc />
    public static byte Id => 25;
}

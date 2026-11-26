namespace MeowSpades.Protocol.Server;

/// <summary>Reports an intel capture event.</summary>
/// <param name="PlayerId">The capturing player's identifier.</param>
/// <param name="Winning">Whether the capture wins the game.</param>
public readonly record struct IntelCapturePacket(byte PlayerId, bool Winning) : IPacket
{
    /// <inheritdoc />
    public static byte Id => 23;
}

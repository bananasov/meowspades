using MeowSpades.Protocol.Enums;

namespace MeowSpades.Protocol.Server;

/// <summary>Sets a player's equipped tool.</summary>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="Tool">The equipped tool.</param>
public readonly record struct SetToolPacket(byte PlayerId, ToolType Tool) : IPacket
{
    /// <inheritdoc />
    public static byte Id => 7;
}

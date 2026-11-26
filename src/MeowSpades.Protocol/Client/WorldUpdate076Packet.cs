using MeowSpades.Protocol.State;

namespace MeowSpades.Protocol.Client;

/// <summary>Updates transforms for the players included in a protocol 0.76 update.</summary>
/// <param name="Players">The player identifiers and transforms included in the update.</param>
public readonly record struct WorldUpdate076Packet(ReadOnlyMemory<PlayerTransform076> Players) : IPacket
{
    /// <inheritdoc />
    public static byte Id => 2;
}

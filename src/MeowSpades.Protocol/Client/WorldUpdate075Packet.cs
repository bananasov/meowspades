using MeowSpades.Protocol.State;

namespace MeowSpades.Protocol.Client;

/// <summary>Updates transforms for the fixed player set used by protocol 0.75.</summary>
/// <param name="Players">Player transforms, with unoccupied slots zeroed.</param>
public readonly record struct WorldUpdate075Packet(ReadOnlyMemory<PlayerTransform> Players) : IPacket
{
    /// <inheritdoc />
    public static byte Id => 2;
}

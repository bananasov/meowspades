using MeowSpades.Protocol.State;

namespace MeowSpades.Protocol.Client;

/// <summary>Updates transforms for the fixed player set used by protocol 0.75.</summary>
/// <param name="Players">Player transforms, with unoccupied slots zeroed.</param>
public readonly record struct WorldUpdate075Packet(
    ReadOnlyMemory<PlayerTransform> Players
) : IPacket<WorldUpdate075Packet>
{
    /// <inheritdoc />
    public static byte Id => 2;

    public static WorldUpdate075Packet Deserialize(ReadOnlySpan<byte> source)
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

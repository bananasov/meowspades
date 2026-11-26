using MeowSpades.Protocol.Enums;

namespace MeowSpades.Protocol.Client;

/// <summary>Places or destroys a block at integer map coordinates.</summary>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="Action">The block action.</param>
/// <param name="X">The X map coordinate.</param>
/// <param name="Y">The Y map coordinate.</param>
/// <param name="Z">The Z map coordinate.</param>
public readonly record struct BlockActionPacket(
    byte PlayerId,
    BlockActionType Action,
    int X,
    int Y,
    int Z
) : IPacket<BlockActionPacket>
{
    /// <inheritdoc />
    public static byte Id => 13;

    public static BlockActionPacket Deserialize(ReadOnlySpan<byte> source)
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

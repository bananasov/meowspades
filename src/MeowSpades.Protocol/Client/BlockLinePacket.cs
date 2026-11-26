namespace MeowSpades.Protocol.Client;

/// <summary>Creates a line of blocks between two map coordinates.</summary>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="StartX">The starting X coordinate.</param>
/// <param name="StartY">The starting Y coordinate.</param>
/// <param name="StartZ">The starting Z coordinate.</param>
/// <param name="EndX">The ending X coordinate.</param>
/// <param name="EndY">The ending Y coordinate.</param>
/// <param name="EndZ">The ending Z coordinate.</param>
public readonly record struct BlockLinePacket(
    byte PlayerId,
    int StartX,
    int StartY,
    int StartZ,
    int EndX,
    int EndY,
    int EndZ
) : IPacket<BlockLinePacket>
{
    /// <inheritdoc />
    public static byte Id => 14;

    public static BlockLinePacket Deserialize(ReadOnlySpan<byte> source)
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

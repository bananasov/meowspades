namespace MeowSpades.Protocol.Server;

/// <summary>Reports that a player has been restocked.</summary>
/// <param name="PlayerId">The player's identifier.</param>
public readonly record struct RestockPacket(byte PlayerId) : IPacket<RestockPacket>
{
    /// <inheritdoc />
    public static byte Id => 26;

    public static RestockPacket Deserialize(ReadOnlySpan<byte> source)
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

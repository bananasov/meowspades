namespace MeowSpades.Protocol.Server;

/// <summary>Reports that a player picked up the intel.</summary>
/// <param name="PlayerId">The player's identifier.</param>
public readonly record struct IntelPickupPacket(byte PlayerId) : IPacket<IntelPickupPacket>
{
    /// <inheritdoc />
    public static byte Id => 24;

    public static IntelPickupPacket Deserialize(ReadOnlySpan<byte> source)
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

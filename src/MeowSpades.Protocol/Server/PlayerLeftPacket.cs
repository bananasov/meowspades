namespace MeowSpades.Protocol.Server;

/// <summary>Notifies clients that a player has disconnected.</summary>
/// <param name="PlayerId">The disconnected player's identifier.</param>
public readonly record struct PlayerLeftPacket(byte PlayerId) : IPacket<PlayerLeftPacket>
{
    /// <inheritdoc />
    public static byte Id => 20;

    public static PlayerLeftPacket Deserialize(ReadOnlySpan<byte> source)
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

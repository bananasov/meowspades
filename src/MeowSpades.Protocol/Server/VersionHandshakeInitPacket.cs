namespace MeowSpades.Protocol.Server;

/// <summary>Starts the extra-packet version handshake.</summary>
/// <param name="Challenge">The signed challenge value to return in the response.</param>
public readonly record struct VersionHandshakeInitPacket(int Challenge) : IPacket<VersionHandshakeInitPacket>
{
    /// <inheritdoc />
    public static byte Id => 31;

    public static VersionHandshakeInitPacket Deserialize(ReadOnlySpan<byte> source)
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

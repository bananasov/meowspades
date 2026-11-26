namespace MeowSpades.Protocol.Client;

/// <summary>Returns the challenge from a version-handshake initialization.</summary>
/// <param name="Challenge">The challenge received from the server.</param>
public readonly record struct VersionHandshakeResponsePacket(
    int Challenge
) : IPacket<VersionHandshakeResponsePacket>
{
    /// <inheritdoc />
    public static byte Id => 32;

    public static VersionHandshakeResponsePacket Deserialize(ReadOnlySpan<byte> source)
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

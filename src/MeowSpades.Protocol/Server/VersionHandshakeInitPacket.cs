namespace MeowSpades.Protocol.Server;

/// <summary>Starts the extra-packet version handshake.</summary>
/// <param name="Challenge">The signed challenge value to return in the response.</param>
public readonly record struct VersionHandshakeInitPacket(int Challenge) : IPacket
{
    /// <inheritdoc />
    public static byte Id => 31;
}

namespace MeowSpades.Protocol.Client;

/// <summary>Returns the challenge from a version-handshake initialization.</summary>
/// <param name="Challenge">The challenge received from the server.</param>
public readonly record struct VersionHandshakeResponsePacket(int Challenge) : IPacket
{
    /// <inheritdoc />
    public static byte Id => 32;
}

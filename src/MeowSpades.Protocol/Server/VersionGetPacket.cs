namespace MeowSpades.Protocol.Server;

/// <summary>Requests version information and optionally selects extended properties.</summary>
/// <param name="PropertyIds">The requested property identifiers, or empty for a classic response.</param>
public readonly record struct VersionGetPacket(ReadOnlyMemory<byte> PropertyIds) : IPacket
{
    /// <inheritdoc />
    public static byte Id => 33;
}

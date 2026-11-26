namespace MeowSpades.Protocol.Client;

/// <summary>Returns a client's version and operating-system information.</summary>
/// <param name="ClientIdentifier">The client's ASCII identifier byte.</param>
/// <param name="VersionMajor">The signed major version component.</param>
/// <param name="VersionMinor">The signed minor version component.</param>
/// <param name="VersionRevision">The signed revision component.</param>
/// <param name="OperatingSystem">The CP437 operating-system name as a .NET string.</param>
public readonly record struct VersionResponsePacket(
    byte ClientIdentifier,
    sbyte VersionMajor,
    sbyte VersionMinor,
    sbyte VersionRevision,
    string OperatingSystem
) : IPacket
{
    /// <inheritdoc />
    public static byte Id => 34;
}

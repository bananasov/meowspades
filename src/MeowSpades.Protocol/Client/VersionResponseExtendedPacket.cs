namespace MeowSpades.Protocol.Client;

/// <summary>Returns the requested extended version properties.</summary>
/// <param name="Properties">The properties in ascending identifier order.</param>
public readonly record struct VersionResponseExtendedPacket(ReadOnlyMemory<VersionProperty> Properties) : IPacket
{
    /// <inheritdoc />
    public static byte Id => 9;

    /// <summary>Gets the marker byte distinguishing this response from existing-player data.</summary>
    public const byte Marker = 0x78;
}

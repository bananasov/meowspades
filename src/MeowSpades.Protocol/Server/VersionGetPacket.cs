namespace MeowSpades.Protocol.Server;

/// <summary>Requests version information and optionally selects extended properties.</summary>
/// <param name="PropertyIds">The requested property identifiers, or empty for a classic response.</param>
public readonly record struct VersionGetPacket(ReadOnlyMemory<byte> PropertyIds) : IPacket<VersionGetPacket>
{
    /// <inheritdoc />
    public static byte Id => 33;

    public static VersionGetPacket Deserialize(ReadOnlySpan<byte> source)
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

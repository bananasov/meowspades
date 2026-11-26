namespace MeowSpades.Protocol.Client;

// TODO: Consider using Vector3 here, its the same anyways.
/// <summary>Sets the sender's world orientation.</summary>
/// <param name="X">The X component.</param>
/// <param name="Y">The Y component.</param>
/// <param name="Z">The Z component.</param>
public readonly record struct OrientationDataPacket(
    float X,
    float Y,
    float Z
) : IPacket<OrientationDataPacket>
{
    /// <inheritdoc />
    public static byte Id => 1;

    public static OrientationDataPacket Deserialize(ReadOnlySpan<byte> source)
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

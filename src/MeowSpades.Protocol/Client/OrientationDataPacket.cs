namespace MeowSpades.Protocol.Client;

/// <summary>Sets the sender's world orientation.</summary>
/// <param name="X">The X component.</param>
/// <param name="Y">The Y component.</param>
/// <param name="Z">The Z component.</param>
public readonly record struct OrientationDataPacket(float X, float Y, float Z) : IPacket
{
    /// <inheritdoc />
    public static byte Id => 1;
}

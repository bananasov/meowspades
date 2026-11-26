namespace MeowSpades.Protocol.Client;

/// <summary>Sets the sender's world position.</summary>
/// <param name="X">The X coordinate.</param>
/// <param name="Y">The Y coordinate.</param>
/// <param name="Z">The Z coordinate.</param>
public readonly record struct PositionDataPacket(float X, float Y, float Z) : IPacket
{
    /// <inheritdoc />
    public static byte Id => 0;
}

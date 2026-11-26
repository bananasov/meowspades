namespace MeowSpades.Protocol.Server;

/// <summary>Sets the client's fog color.</summary>
/// <param name="Alpha">The unused alpha component.</param>
/// <param name="Blue">The blue component.</param>
/// <param name="Green">The green component.</param>
/// <param name="Red">The red component.</param>
public readonly record struct FogColorPacket(byte Alpha, byte Blue, byte Green, byte Red) : IPacket
{
    /// <inheritdoc />
    public static byte Id => 27;
}

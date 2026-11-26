namespace MeowSpades.Protocol.Client;

/// <summary>Reports whether the map is cached by the client.</summary>
/// <param name="Cached">Whether the map is cached locally.</param>
public readonly record struct MapCachedPacket(bool Cached) : IPacket
{
    /// <inheritdoc />
    public static byte Id => 31;
}

namespace MeowSpades.Protocol.Server;

/// <summary>Begins a protocol 0.75 map transfer.</summary>
/// <param name="MapSize">The uncompressed map size in bytes.</param>
public readonly record struct MapStart075Packet(uint MapSize) : IPacket
{
    /// <inheritdoc />
    public static byte Id => 18;
}

namespace MeowSpades.Protocol.Server;

/// <summary>Begins a protocol 0.76 map transfer.</summary>
/// <param name="MapSize">The uncompressed map size in bytes.</param>
/// <param name="Crc32">The map CRC-32 value.</param>
/// <param name="MapName">The CP437 map name as a .NET string.</param>
public readonly record struct MapStart076Packet(uint MapSize, uint Crc32, string MapName) : IPacket
{
    /// <inheritdoc />
    public static byte Id => 18;
}

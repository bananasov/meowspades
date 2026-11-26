namespace MeowSpades.Protocol.Server;

/// <summary>Begins a protocol 0.75 map transfer.</summary>
/// <param name="MapSize">The uncompressed map size in bytes.</param>
public readonly record struct MapStart075Packet(uint MapSize) : IPacket<MapStart075Packet>
{
    /// <inheritdoc />
    public static byte Id => 18;

    public static MapStart075Packet Deserialize(ReadOnlySpan<byte> source)
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

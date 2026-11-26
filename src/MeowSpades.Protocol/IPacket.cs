namespace MeowSpades.Protocol;

/// <summary>Defines a packet with a statically known wire identifier.</summary>
public interface IPacket<TSelf> where TSelf : IPacket<TSelf>
{
    /// <summary>Gets the packet identifier sent as the first byte.</summary>
    static abstract byte Id { get; }

    /// <summary>Gets the maximum number of bytes required to serialize this packet.</summary>
    /// <returns>The maximum serialized size, including the packet identifier.</returns>
    int GetMaxSize();

    /// <summary>Serializes this packet into the specified destination.</summary>
    /// <param name="destination">The buffer that receives the serialized packet.</param>
    /// <returns>The number of bytes written to <paramref name="destination"/>.</returns>
    int Serialize(Span<byte> destination);

    /// <summary>Deserializes a packet from its wire representation.</summary>
    /// <param name="source">The serialized packet data, including its packet identifier.</param>
    /// <returns>The deserialized packet.</returns>
    static abstract TSelf Deserialize(ReadOnlySpan<byte> source);
}

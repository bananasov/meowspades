namespace MeowSpades.Protocol;

/// <summary>Defines a packet with a statically known wire identifier.</summary>
public interface IPacket
{
    /// <summary>Gets the packet identifier sent as the first byte.</summary>
    static abstract byte Id { get; }
}

namespace MeowSpades.Protocol.Client;

/// <summary>Represents one property in an extended version response.</summary>
/// <param name="Id">The property identifier.</param>
/// <param name="Payload">The property payload bytes.</param>
public readonly record struct VersionProperty(byte Id, ReadOnlyMemory<byte> Payload);

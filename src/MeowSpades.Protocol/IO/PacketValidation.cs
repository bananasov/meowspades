namespace MeowSpades.Protocol.IO;

/// <summary>Provides validation helpers for serialized protocol packets.</summary>
internal static class PacketValidation
{
    /// <summary>Throws when the serialized packet does not have the expected size.</summary>
    /// <param name="source">The serialized packet data.</param>
    /// <param name="expectedSize">The expected packet size in bytes.</param>
    /// <exception cref="ArgumentException">The source length does not equal <paramref name="expectedSize"/>.</exception>
    internal static void ThrowIfInvalidLength(ReadOnlySpan<byte> source, int expectedSize)
    {
        if (source.Length != expectedSize)
        {
            throw new ArgumentException(
                $"Serialized packet data must be exactly {expectedSize} bytes.",
                nameof(source));
        }
    }

    /// <summary>Throws when the packet identifier does not match the expected identifier.</summary>
    /// <param name="actualId">The identifier read from the packet.</param>
    /// <param name="expectedId">The expected identifier.</param>
    /// <exception cref="InvalidDataException">The identifiers do not match.</exception>
    internal static void ThrowIfInvalidPacketId(byte actualId, byte expectedId)
    {
        if (actualId != expectedId)
        {
            throw new InvalidDataException(
                $"Invalid packet identifier. Expected {expectedId}, received {actualId}.");
        }
    }
}

using MeowSpades.Binary;
using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Server;

/// <summary>Requests version information and optionally selects extended properties.</summary>
/// <param name="PropertyIds">The requested property identifiers, or empty for a classic response.</param>
public readonly record struct VersionGetPacket(ReadOnlyMemory<byte> PropertyIds) : IPacket<VersionGetPacket>
{
    /// <inheritdoc />
    public static byte Id => 33;

    /// <inheritdoc />
    public static VersionGetPacket Deserialize(ReadOnlySpan<byte> source)
    {
        PacketValidation.ThrowIfTooShort(source, sizeof(byte));

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var propertyIds = reader.ReadBytes(reader.Remaining);
        return new VersionGetPacket(propertyIds.ToArray());
    }

    /// <inheritdoc />
    public int GetMaxSize() => 1 + PropertyIds.Length;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteBytes(PropertyIds.Span);

        return writer.WrittenCount;
    }
}

using System.Text;
using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Client;

/// <summary>Returns a client's version and operating-system information.</summary>
/// <param name="ClientIdentifier">The client's ASCII identifier byte.</param>
/// <param name="VersionMajor">The signed major version component.</param>
/// <param name="VersionMinor">The signed minor version component.</param>
/// <param name="VersionRevision">The signed revision component.</param>
/// <param name="OperatingSystem">The CP437 operating-system name as a .NET string.</param>
public readonly record struct VersionResponsePacket(
    byte ClientIdentifier,
    sbyte VersionMajor,
    sbyte VersionMinor,
    sbyte VersionRevision,
    string OperatingSystem
) : IPacket<VersionResponsePacket>
{
    private const int FixedSize = 5;
    private static readonly Encoding Cp437 = Encoding.GetEncoding(437);

    /// <inheritdoc />
    public static byte Id => 34;

    /// <inheritdoc />
    public static VersionResponsePacket Deserialize(ReadOnlySpan<byte> source)
    {
        PacketValidation.ThrowIfTooShort(source, FixedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var clientIdentifier = reader.ReadByte();
        var versionMajor = reader.ReadSByte();
        var versionMinor = reader.ReadSByte();
        var versionRevision = reader.ReadSByte();

        var operatingSystemBytes = reader.ReadBytes(reader.Remaining);
        var operatingSystem = Cp437.GetString(operatingSystemBytes);

        return new VersionResponsePacket(
            clientIdentifier,
            versionMajor,
            versionMinor,
            versionRevision,
            operatingSystem);
    }

    /// <inheritdoc />
    public int GetMaxSize() => FixedSize + Cp437.GetByteCount(OperatingSystem);

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(ClientIdentifier);
        writer.WriteSByte(VersionMajor);
        writer.WriteSByte(VersionMinor);
        writer.WriteSByte(VersionRevision);
        writer.WriteBytes(Cp437.GetBytes(OperatingSystem));

        return writer.WrittenCount;
    }
}

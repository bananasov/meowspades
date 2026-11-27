using MeowSpades.Binary;
using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Client;

/// <summary>Returns the requested extended version properties.</summary>
/// <param name="Properties">The properties in ascending identifier order.</param>
public readonly record struct VersionResponseExtendedPacket(
    ReadOnlyMemory<VersionProperty> Properties
) : IPacket<VersionResponseExtendedPacket>
{
    /// <inheritdoc />
    public static byte Id => 9;

    /// <summary>Gets the marker byte distinguishing this response from existing-player data.</summary>
    public const byte Marker = 0x78;

    /// <inheritdoc />
    public int GetMaxSize()
    {
        int size = 2;

        foreach (VersionProperty property in Properties.Span)
        {
            size = checked(size + 2 + property.Payload.Length);
        }

        return size;
    }

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        foreach (VersionProperty property in Properties.Span)
        {
            if (property.Payload.Length > byte.MaxValue)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(Properties),
                    "A version property payload cannot exceed 255 bytes.");
            }
        }

        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(Marker);

        foreach (VersionProperty property in Properties.Span)
        {
            writer.WriteByte(property.Id);
            writer.WriteByte((byte)property.Payload.Length);
            writer.WriteBytes(property.Payload.Span);
        }

        return writer.WrittenCount;
    }

    /// <inheritdoc />
    public static VersionResponseExtendedPacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int HeaderSize = 2;
        PacketValidation.ThrowIfTooShort(source, HeaderSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        byte marker = reader.ReadByte();
        if (marker != Marker)
        {
            throw new InvalidDataException("The data is not an extended version response packet.");
        }

        var properties = new List<VersionProperty>();
        while (reader.Remaining > 0)
        {
            PacketValidation.ThrowIfTooShort(reader.RemainingSpan, 2);

            var propertyId = reader.ReadByte();
            var payloadLength = reader.ReadByte();
            var payloadBytes = reader.ReadBytes(payloadLength);
            var payload = payloadBytes.ToArray();

            properties.Add(new VersionProperty(propertyId, payload));
        }

        return new VersionResponseExtendedPacket(properties.ToArray());
    }
}

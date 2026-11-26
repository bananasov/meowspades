using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Server;

/// <summary>Sets the client's fog color.</summary>
/// <param name="Alpha">The unused alpha component.</param>
/// <param name="Blue">The blue component.</param>
/// <param name="Green">The green component.</param>
/// <param name="Red">The red component.</param>
public readonly record struct FogColorPacket(byte Alpha, byte Blue, byte Green, byte Red) : IPacket<FogColorPacket>
{
    /// <inheritdoc />
    public static byte Id => 27;

    /// <inheritdoc />
    public int GetMaxSize() => 5;

    /// <inheritdoc />
    public static FogColorPacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 5;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var alpha = reader.ReadByte();
        var blue = reader.ReadByte();
        var green = reader.ReadByte();
        var red = reader.ReadByte();

        return new FogColorPacket(alpha, blue, green, red);
    }

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(Alpha);
        writer.WriteByte(Blue);
        writer.WriteByte(Green);
        writer.WriteByte(Red);

        return writer.WrittenCount;
    }
}

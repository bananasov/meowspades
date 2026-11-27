using MeowSpades.Protocol.IO;
using MeowSpades.Protocol.Models;

namespace MeowSpades.Protocol.Server;

/// <summary>Sets the client's fog color.</summary>
/// <param name="Alpha">The unused alpha component.</param>
/// <param name="Color">The fog color</param>
public readonly record struct FogColorPacket(byte Alpha, Color Color) : IPacket<FogColorPacket>
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
        var color = reader.ReadColor();

        return new FogColorPacket(alpha, color);
    }

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(Alpha);
        writer.WriteColor(Color);

        return writer.WrittenCount;
    }
}

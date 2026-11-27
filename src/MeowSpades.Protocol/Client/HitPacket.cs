using MeowSpades.Protocol.Enums;
using MeowSpades.Binary;
using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Client;

/// <summary>Reports a hit to the server.</summary>
/// <param name="TargetPlayerId">The player identifier that was hit.</param>
/// <param name="Type">The hit region or attack type.</param>
public readonly record struct HitPacket(byte TargetPlayerId, HitType Type) : IPacket<HitPacket>
{
    /// <inheritdoc />
    public static byte Id => 5;

    /// <inheritdoc />
    public static HitPacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 3;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        byte targetPlayerId = reader.ReadByte();
        var hitType = (HitType)reader.ReadByte();

        return new HitPacket(targetPlayerId, hitType);
    }

    /// <inheritdoc />
    public int GetMaxSize() => 3;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(TargetPlayerId);
        writer.WriteByte((byte)Type);

        return writer.WrittenCount;
    }
}

using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Server;

/// <summary>Sets health and identifies the source of damage.</summary>
/// <param name="Health">The resulting health value.</param>
/// <param name="DamageType">Zero for fall damage or one for weapon damage.</param>
/// <param name="SourceX">The source X coordinate.</param>
/// <param name="SourceY">The source Y coordinate.</param>
/// <param name="SourceZ">The source Z coordinate.</param>
public readonly record struct SetHealthPacket(
    byte Health,
    byte DamageType,
    float SourceX,
    float SourceY,
    float SourceZ
) : IPacket<SetHealthPacket>
{
    /// <inheritdoc />
    public static byte Id => 5;

    /// <inheritdoc />
    public static SetHealthPacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 15;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var health = reader.ReadByte();
        var damageType = reader.ReadByte();
        var sourceX = reader.ReadFloat();
        var sourceY = reader.ReadFloat();
        var sourceZ = reader.ReadFloat();

        return new SetHealthPacket(health, damageType, sourceX, sourceY, sourceZ);
    }

    /// <inheritdoc />
    public int GetMaxSize() => 15;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(Health);
        writer.WriteByte(DamageType);
        writer.WriteFloat(SourceX);
        writer.WriteFloat(SourceY);
        writer.WriteFloat(SourceZ);

        return writer.WrittenCount;
    }
}

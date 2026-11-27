using System.Numerics;
using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Server;

/// <summary>Sets health and identifies the source of damage.</summary>
/// <param name="Health">The resulting health value.</param>
/// <param name="DamageType">Zero for fall damage or one for weapon damage.</param>
/// <param name="SourcePosition">The position from which the damage originated.</param>
public readonly record struct SetHealthPacket(
    byte Health,
    byte DamageType,
    Vector3 SourcePosition
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
        var sourcePosition = reader.ReadVector3();

        return new SetHealthPacket(
            health,
            damageType,
            sourcePosition
        );
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
        writer.WriteVector3(SourcePosition);

        return writer.WrittenCount;
    }
}

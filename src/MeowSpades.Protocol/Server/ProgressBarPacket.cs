using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Server;

/// <summary>Updates a Territory Control progress bar.</summary>
/// <param name="EntityId">The command post identifier.</param>
/// <param name="CapturingTeamId">The capturing team identifier.</param>
/// <param name="Rate">The signed progress rate.</param>
/// <param name="Progress">Progress from zero to one.</param>
public readonly record struct ProgressBarPacket(
    byte EntityId,
    byte CapturingTeamId,
    sbyte Rate,
    float Progress
) : IPacket<ProgressBarPacket>
{
    /// <inheritdoc />
    public static byte Id => 22;

    /// <inheritdoc />
    public static ProgressBarPacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 8;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var entityId = reader.ReadByte();
        var capturingTeamId = reader.ReadByte();
        var rate = reader.ReadSByte();
        var progress = reader.ReadFloat();

        return new ProgressBarPacket(entityId, capturingTeamId, rate, progress);
    }

    /// <inheritdoc />
    public int GetMaxSize() => 8;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(EntityId);
        writer.WriteByte(CapturingTeamId);
        writer.WriteSByte(Rate);
        writer.WriteFloat(Progress);

        return writer.WrittenCount;
    }
}

using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Server;

/// <summary>Reports a Territory Control capture event.</summary>
/// <param name="PlayerId">The capturing player's identifier.</param>
/// <param name="EntityId">The captured command post identifier.</param>
/// <param name="Winning">Whether the capture wins the game.</param>
/// <param name="State">The team identifier associated with the capture.</param>
public readonly record struct TerritoryCapturePacket(
    byte PlayerId,
    byte EntityId,
    bool Winning,
    byte State
) : IPacket<TerritoryCapturePacket>
{
    /// <inheritdoc />
    public static byte Id => 21;

    /// <inheritdoc />
    public static TerritoryCapturePacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 5;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var playerId = reader.ReadByte();
        var entityId = reader.ReadByte();
        var winning = reader.ReadBoolean();
        var state = reader.ReadByte();

        return new TerritoryCapturePacket(playerId, entityId, winning, state);
    }

    /// <inheritdoc />
    public int GetMaxSize() => 5;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(PlayerId);
        writer.WriteByte(EntityId);
        writer.WriteBoolean(Winning);
        writer.WriteByte(State);

        return writer.WrittenCount;
    }
}

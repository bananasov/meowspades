using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Client;

/// <summary>Requests a team change.</summary>
/// <param name="PlayerId">The player's identifier.</param>
/// <param name="Team">The requested team identifier.</param>
public readonly record struct ChangeTeamPacket(
    byte PlayerId,
    byte Team
) : IPacket<ChangeTeamPacket>
{
    /// <inheritdoc />
    public static byte Id => 29;

    /// <inheritdoc />
    public int GetMaxSize() => 3;

    /// <inheritdoc />
    public static ChangeTeamPacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 3;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var playerId = reader.ReadByte();
        var team = reader.ReadByte();

        return new ChangeTeamPacket(playerId, team);
    }

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(PlayerId);
        writer.WriteByte(Team);

        return writer.WrittenCount;
    }
}

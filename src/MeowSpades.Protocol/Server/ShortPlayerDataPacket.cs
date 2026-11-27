using MeowSpades.Binary;
using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Server;

/// <summary>Announces a player's identifier, team, and weapon.</summary>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="Team">The team identifier.</param>
/// <param name="Weapon">The weapon identifier.</param>
public readonly record struct ShortPlayerDataPacket(byte PlayerId, byte Team, byte Weapon) : IPacket<ShortPlayerDataPacket>
{
    /// <inheritdoc />
    public static byte Id => 10;

    /// <inheritdoc />
    public static ShortPlayerDataPacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 4;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var playerId = reader.ReadByte();
        var team = reader.ReadByte();
        var weapon = reader.ReadByte();

        return new ShortPlayerDataPacket(playerId, team, weapon);
    }

    /// <inheritdoc />
    public int GetMaxSize() => 4;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(PlayerId);
        writer.WriteByte(Team);
        writer.WriteByte(Weapon);

        return writer.WrittenCount;
    }
}

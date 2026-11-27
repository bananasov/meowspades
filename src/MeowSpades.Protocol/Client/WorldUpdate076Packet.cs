
using MeowSpades.Binary;
using MeowSpades.Protocol.IO;
using MeowSpades.Protocol.State;

namespace MeowSpades.Protocol.Client;

/// <summary>Updates transforms for the players included in a protocol 0.76 update.</summary>
/// <param name="Players">The player identifiers and transforms included in the update.</param>
public readonly record struct WorldUpdate076Packet(
    ReadOnlyMemory<PlayerTransform076> Players
) : IPacket<WorldUpdate076Packet>
{
    private const int PlayerTransformSize = 25;

    /// <inheritdoc />
    public static byte Id => 2;

    /// <inheritdoc />
    public static WorldUpdate076Packet Deserialize(ReadOnlySpan<byte> source)
    {
        PacketValidation.ThrowIfTooShort(source, sizeof(byte));

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        if (reader.Remaining % PlayerTransformSize != 0)
        {
            throw new InvalidDataException(
                "A protocol 0.76 world update must contain complete player transform entries.");
        }

        var players = new PlayerTransform076[reader.Remaining / PlayerTransformSize];
        for (int i = 0; i < players.Length; i++)
        {
            byte playerId = reader.ReadByte();
            var position = reader.ReadVector3();
            var orientation = reader.ReadVector3();

            players[i] = new PlayerTransform076(playerId, position, orientation);
        }

        return new WorldUpdate076Packet(players);
    }

    /// <inheritdoc />
    public int GetMaxSize() => checked(1 + (PlayerTransformSize * Players.Length));

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);

        foreach (PlayerTransform076 player in Players.Span)
        {
            writer.WriteByte(player.PlayerId);
            writer.WriteVector3(player.Position);
            writer.WriteVector3(player.Orientation);
        }

        return writer.WrittenCount;
    }
}

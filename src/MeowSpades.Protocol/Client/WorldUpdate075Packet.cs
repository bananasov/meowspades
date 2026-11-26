using System.Numerics;
using MeowSpades.Protocol.IO;
using MeowSpades.Protocol.State;

namespace MeowSpades.Protocol.Client;

/// <summary>Updates transforms for the fixed player set used by protocol 0.75.</summary>
/// <param name="Players">Player transforms, with unoccupied slots zeroed.</param>
public readonly record struct WorldUpdate075Packet(
    ReadOnlyMemory<PlayerTransform> Players
) : IPacket<WorldUpdate075Packet>
{
    private const int PlayerCount = 32;
    private const int PlayerTransformSize = 24;
    private const int SerializedSize = 1 + (PlayerCount * PlayerTransformSize);

    /// <inheritdoc />
    public static byte Id => 2;

    /// <inheritdoc />
    public static WorldUpdate075Packet Deserialize(ReadOnlySpan<byte> source)
    {
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var players = new PlayerTransform[PlayerCount];
        for (int i = 0; i < players.Length; i++)
        {
            float positionX = reader.ReadFloat();
            float positionY = reader.ReadFloat();
            float positionZ = reader.ReadFloat();
            float orientationX = reader.ReadFloat();
            float orientationY = reader.ReadFloat();
            float orientationZ = reader.ReadFloat();

            var position = new Vector3(positionX, positionY, positionZ);
            var orientation = new Vector3(orientationX, orientationY, orientationZ);

            players[i] = new PlayerTransform(position, orientation);
        }

        return new WorldUpdate075Packet(players);
    }

    /// <inheritdoc />
    public int GetMaxSize() => SerializedSize;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        if (Players.Length != PlayerCount)
        {
            throw new ArgumentException(
                $"A protocol 0.75 world update must contain exactly {PlayerCount} player transforms.",
                nameof(Players));
        }

        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);

        foreach (PlayerTransform player in Players.Span)
        {
            writer.WriteFloat(player.Position.X);
            writer.WriteFloat(player.Position.Y);
            writer.WriteFloat(player.Position.Z);
            writer.WriteFloat(player.Orientation.X);
            writer.WriteFloat(player.Orientation.Y);
            writer.WriteFloat(player.Orientation.Z);
        }

        return writer.WrittenCount;
    }
}

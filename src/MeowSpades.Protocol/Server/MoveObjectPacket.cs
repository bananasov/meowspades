using System.Numerics;
using MeowSpades.Binary;
using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Server;

/// <summary>Moves a game object such as a tent, intel, or grenade.</summary>
/// <param name="ObjectId">The object identifier.</param>
/// <param name="Team">The object team.</param>
/// <param name="Position">The object's world position.</param>
public readonly record struct MoveObjectPacket(
    byte ObjectId,
    byte Team,
    Vector3 Position
) : IPacket<MoveObjectPacket>
{
    /// <inheritdoc />
    public static byte Id => 11;

    /// <inheritdoc />
    public static MoveObjectPacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 15;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var objectId = reader.ReadByte();
        var team = reader.ReadByte();
        var position = reader.ReadVector3();

        return new MoveObjectPacket(objectId, team, position);
    }

    /// <inheritdoc />
    public int GetMaxSize() => 15;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(ObjectId);
        writer.WriteByte(Team);
        writer.WriteVector3(Position);

        return writer.WrittenCount;
    }
}

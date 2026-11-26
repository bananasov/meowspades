using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Server;

/// <summary>Moves a game object such as a tent, intel, or grenade.</summary>
/// <param name="ObjectId">The object identifier.</param>
/// <param name="Team">The object team.</param>
/// <param name="X">The X coordinate.</param>
/// <param name="Y">The Y coordinate.</param>
/// <param name="Z">The Z coordinate.</param>
public readonly record struct MoveObjectPacket(
    byte ObjectId,
    byte Team,
    float X,
    float Y,
    float Z
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
        var x = reader.ReadFloat();
        var y = reader.ReadFloat();
        var z = reader.ReadFloat();

        return new MoveObjectPacket(objectId, team, x, y, z);
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
        writer.WriteFloat(X);
        writer.WriteFloat(Y);
        writer.WriteFloat(Z);

        return writer.WrittenCount;
    }
}

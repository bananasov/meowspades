using System.Numerics;
using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Client;

/// <summary>Spawns a grenade with its fuse, position, and velocity.</summary>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="FuseLength">The fuse duration.</param>
/// <param name="Position">The grenade's world position.</param>
/// <param name="Velocity">The grenade's velocity.</param>
public readonly record struct GrenadePacket(
    byte PlayerId,
    float FuseLength,
    Vector3 Position,
    Vector3 Velocity
) : IPacket<GrenadePacket>
{
    /// <inheritdoc />
    public static byte Id => 6;

    /// <inheritdoc />
    public static GrenadePacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 30;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        byte playerId = reader.ReadByte();
        float fuseLength = reader.ReadFloat();
        var position = reader.ReadVector3();
        var velocity = reader.ReadVector3();

        return new GrenadePacket(
            playerId,
            fuseLength,
            position,
            velocity
        );
    }

    /// <inheritdoc />
    public int GetMaxSize() => 30;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(PlayerId);
        writer.WriteFloat(FuseLength);
        writer.WriteVector3(Position);
        writer.WriteVector3(Velocity);

        return writer.WrittenCount;
    }
}

using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Client;

/// <summary>Spawns a grenade with its fuse, position, and velocity.</summary>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="FuseLength">The fuse duration.</param>
/// <param name="X">The X coordinate.</param>
/// <param name="Y">The Y coordinate.</param>
/// <param name="Z">The Z coordinate.</param>
/// <param name="VelocityX">The X velocity.</param>
/// <param name="VelocityY">The Y velocity.</param>
/// <param name="VelocityZ">The Z velocity.</param>
public readonly record struct GrenadePacket(
    byte PlayerId,
    float FuseLength,
    float X,
    float Y,
    float Z,
    float VelocityX,
    float VelocityY,
    float VelocityZ
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
            position.X,
            position.Y,
            position.Z,
            velocity.X,
            velocity.Y,
            velocity.Z);
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
        writer.WriteVector3(X, Y, Z);
        writer.WriteVector3(VelocityX, VelocityY, VelocityZ);

        return writer.WrittenCount;
    }
}

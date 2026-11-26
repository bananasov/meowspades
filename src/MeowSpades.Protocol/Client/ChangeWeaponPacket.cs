using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Client;

/// <summary>Requests a weapon change.</summary>
/// <param name="PlayerId">The player's identifier.</param>
/// <param name="WeaponId">The requested weapon identifier.</param>
public readonly record struct ChangeWeaponPacket(
    byte PlayerId,
    byte WeaponId
) : IPacket<ChangeWeaponPacket>
{
    /// <inheritdoc />
    public static byte Id => 30;

    /// <inheritdoc />
    public int GetMaxSize() => 3;

    public static ChangeWeaponPacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 3;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var playerId = reader.ReadByte();
        var weaponId = reader.ReadByte();

        return new ChangeWeaponPacket(playerId, weaponId);
    }

    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(PlayerId);
        writer.WriteByte(WeaponId);

        return writer.WrittenCount;
    }
}

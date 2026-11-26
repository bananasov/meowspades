using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Server;

/// <summary>Reports a player's weapon reload.</summary>
/// <param name="PlayerId">The reloading player's identifier.</param>
/// <param name="ClipAmmo">The remaining clip ammunition.</param>
/// <param name="ReserveAmmo">The remaining reserve ammunition.</param>
public readonly record struct WeaponReloadPacket(
    byte PlayerId,
    byte ClipAmmo,
    byte ReserveAmmo
) : IPacket<WeaponReloadPacket>
{
    /// <inheritdoc />
    public static byte Id => 28;

    /// <inheritdoc />
    public static WeaponReloadPacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 4;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var playerId = reader.ReadByte();
        var clipAmmo = reader.ReadByte();
        var reserveAmmo = reader.ReadByte();

        return new WeaponReloadPacket(playerId, clipAmmo, reserveAmmo);
    }

    /// <inheritdoc />
    public int GetMaxSize() => 4;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(PlayerId);
        writer.WriteByte(ClipAmmo);
        writer.WriteByte(ReserveAmmo);

        return writer.WrittenCount;
    }
}

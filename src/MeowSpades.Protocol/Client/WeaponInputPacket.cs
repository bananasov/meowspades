using MeowSpades.Protocol;
using MeowSpades.Protocol.Enums;
using MeowSpades.Binary;
using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Client;

/// <summary>Updates a player's weapon button states.</summary>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="Buttons">The pressed weapon buttons.</param>
public readonly record struct WeaponInputPacket(byte PlayerId, WeaponButtonState Buttons) : IPacket<WeaponInputPacket>
{
    /// <inheritdoc />
    public static byte Id => 4;

    /// <inheritdoc />
    public static WeaponInputPacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 3;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        byte playerId = reader.ReadByte();
        var buttons = (WeaponButtonState)reader.ReadByte();

        return new WeaponInputPacket(playerId, buttons);
    }

    /// <inheritdoc />
    public int GetMaxSize() => 3;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(PlayerId);
        writer.WriteByte((byte)Buttons);

        return writer.WrittenCount;
    }
}

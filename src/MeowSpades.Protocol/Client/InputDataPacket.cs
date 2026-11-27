using MeowSpades.Protocol.Enums;
using MeowSpades.Binary;
using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Client;

/// <summary>Updates a player's movement key states.</summary>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="Keys">The pressed movement keys.</param>
public readonly record struct InputDataPacket(
    byte PlayerId,
    InputKeyState Keys
) : IPacket<InputDataPacket>
{
    /// <inheritdoc />
    public static byte Id => 3;

    /// <inheritdoc />
    public static InputDataPacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 3;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var playerId = reader.ReadByte();
        var keys = (InputKeyState)reader.ReadByte();

        return new InputDataPacket(playerId, keys);
    }

    /// <inheritdoc />
    public int GetMaxSize() => 3;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(PlayerId);
        writer.WriteByte((byte)Keys);

        return writer.WrittenCount;
    }
}

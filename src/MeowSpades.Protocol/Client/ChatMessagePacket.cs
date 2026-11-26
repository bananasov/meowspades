using System.Text;
using MeowSpades.Protocol.Enums;
using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Client;

/// <summary>Sends a chat message.</summary>
/// <param name="PlayerId">The sending player identifier.</param>
/// <param name="Type">The chat destination.</param>
/// <param name="Message">The CP437-encoded message as a .NET string.</param>
public readonly record struct ChatMessagePacket(
    byte PlayerId,
    ChatType Type,
    string Message
) : IPacket<ChatMessagePacket>
{
    private static readonly Encoding Cp437 = Encoding.GetEncoding(437);

    /// <inheritdoc />
    public static byte Id => 17;

    /// <inheritdoc />
    public int GetMaxSize() => 3 + 2048; // The initial 3 bytes (Id, player id and type) + the max length.

    /// <inheritdoc />
    public static ChatMessagePacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int HeaderSize = 3;
        PacketValidation.ThrowIfTooShort(source, HeaderSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var playerId = reader.ReadByte();
        var chatType = (ChatType)reader.ReadByte();

        var messageBytes = reader.ReadBytes(reader.Remaining);
        var message = Cp437.GetString(messageBytes);

        return new ChatMessagePacket(playerId, chatType, message);
    }

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(PlayerId);
        writer.WriteByte((byte)Type);

        // TODO: Length validation
        var bytes = Cp437.GetBytes(Message);
        writer.WriteBytes(bytes);

        return writer.WrittenCount;
    }
}

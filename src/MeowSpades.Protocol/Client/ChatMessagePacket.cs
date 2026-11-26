using MeowSpades.Protocol.Enums;

namespace MeowSpades.Protocol.Client;

/// <summary>Sends a chat message.</summary>
/// <param name="PlayerId">The sending player identifier.</param>
/// <param name="Type">The chat destination.</param>
/// <param name="Message">The CP437-encoded message as a .NET string.</param>
public readonly record struct ChatMessagePacket(byte PlayerId, ChatType Type, string Message) : IPacket
{
    /// <inheritdoc />
    public static byte Id => 17;
}

using MeowSpades.Protocol.Enums;

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

    public static InputDataPacket Deserialize(ReadOnlySpan<byte> source)
    {
        throw new NotImplementedException();
    }

    public int GetMaxSize()
    {
        throw new NotImplementedException();
    }

    public int Serialize(Span<byte> destination)
    {
        throw new NotImplementedException();
    }
}

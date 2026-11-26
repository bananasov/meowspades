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

    public static MoveObjectPacket Deserialize(ReadOnlySpan<byte> source)
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

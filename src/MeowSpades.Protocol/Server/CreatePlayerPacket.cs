namespace MeowSpades.Protocol.Server;

/// <summary>Creates or respawns a player.</summary>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="Weapon">The weapon identifier.</param>
/// <param name="Team">The team identifier.</param>
/// <param name="X">The spawn X coordinate.</param>
/// <param name="Y">The spawn Y coordinate.</param>
/// <param name="Z">The spawn Z coordinate.</param>
/// <param name="Name">The CP437-encoded player name as a .NET string.</param>
public readonly record struct CreatePlayerPacket(
    byte PlayerId,
    byte Weapon,
    byte Team,
    float X,
    float Y,
    float Z,
    string Name
) : IPacket<CreatePlayerPacket>
{
    /// <inheritdoc />
    public static byte Id => 12;

    public int GetMaxSize()
    {
        throw new NotImplementedException();
    }

    public static CreatePlayerPacket Deserialize(ReadOnlySpan<byte> source)
    {
        throw new NotImplementedException();
    }

    public int Serialize(Span<byte> destination)
    {
        throw new NotImplementedException();
    }
}

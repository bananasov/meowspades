namespace MeowSpades.Protocol.Client;

/// <summary>Requests a team change.</summary>
/// <param name="PlayerId">The player's identifier.</param>
/// <param name="Team">The requested team identifier.</param>
public readonly record struct ChangeTeamPacket(
    byte PlayerId,
    byte Team
) : IPacket<ChangeTeamPacket>
{
    /// <inheritdoc />
    public static byte Id => 29;

    public static ChangeTeamPacket Deserialize(ReadOnlySpan<byte> source)
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

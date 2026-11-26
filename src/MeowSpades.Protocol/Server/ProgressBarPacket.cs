namespace MeowSpades.Protocol.Server;

/// <summary>Updates a Territory Control progress bar.</summary>
/// <param name="EntityId">The command post identifier.</param>
/// <param name="CapturingTeamId">The capturing team identifier.</param>
/// <param name="Rate">The signed progress rate.</param>
/// <param name="Progress">Progress from zero to one.</param>
public readonly record struct ProgressBarPacket(
    byte EntityId,
    byte CapturingTeamId,
    sbyte Rate,
    float Progress
) : IPacket<ProgressBarPacket>
{
    /// <inheritdoc />
    public static byte Id => 22;

    public static ProgressBarPacket Deserialize(ReadOnlySpan<byte> source)
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

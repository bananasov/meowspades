namespace MeowSpades.Protocol.Server;

/// <summary>Reports an intel capture event.</summary>
/// <param name="PlayerId">The capturing player's identifier.</param>
/// <param name="Winning">Whether the capture wins the game.</param>
public readonly record struct IntelCapturePacket(byte PlayerId, bool Winning) : IPacket<IntelCapturePacket>
{
    /// <inheritdoc />
    public static byte Id => 23;

    public int GetMaxSize()
    {
        throw new NotImplementedException();
    }

    public static IntelCapturePacket Deserialize(ReadOnlySpan<byte> source)
    {
        throw new NotImplementedException();
    }

    public int Serialize(Span<byte> destination)
    {
        throw new NotImplementedException();
    }
}

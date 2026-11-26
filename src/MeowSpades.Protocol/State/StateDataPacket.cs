using MeowSpades.Protocol.Enums;

namespace MeowSpades.Protocol.State;

/// <summary>Contains game parameters and the mode-specific state sent after map transfer.</summary>
/// <param name="PlayerId">The local player's identifier.</param>
/// <param name="FogBlue">The fog blue component.</param>
/// <param name="FogGreen">The fog green component.</param>
/// <param name="FogRed">The fog red component.</param>
/// <param name="Team1Blue">The first team's blue component.</param>
/// <param name="Team1Green">The first team's green component.</param>
/// <param name="Team1Red">The first team's red component.</param>
/// <param name="Team2Blue">The second team's blue component.</param>
/// <param name="Team2Green">The second team's green component.</param>
/// <param name="Team2Red">The second team's red component.</param>
/// <param name="Team1Name">The first team's fixed-width CP437 name.</param>
/// <param name="Team2Name">The second team's fixed-width CP437 name.</param>
/// <param name="Mode">The game mode.</param>
/// <param name="CaptureTheFlag">The CTF state when appropriate.</param>
/// <param name="Territories">The TC territories when appropriate.</param>
public readonly record struct StateDataPacket(
    byte PlayerId,
    byte FogBlue,
    byte FogGreen,
    byte FogRed,
    byte Team1Blue,
    byte Team1Green,
    byte Team1Red,
    byte Team2Blue,
    byte Team2Green,
    byte Team2Red,
    string Team1Name,
    string Team2Name,
    GameMode Mode,
    CaptureTheFlagState? CaptureTheFlag,
    ReadOnlyMemory<TerritoryState> Territories
) : IPacket<StateDataPacket>
{
    /// <inheritdoc />
    public static byte Id => 15;

    public int GetMaxSize()
    {
        throw new NotImplementedException();
    }

    public int Serialize(Span<byte> destination)
    {
        throw new NotImplementedException();
    }

    public static StateDataPacket Deserialize(ReadOnlySpan<byte> source)
    {
        throw new NotImplementedException();
    }
}

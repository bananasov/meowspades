using MeowSpades.Network;

namespace MeowSpades.Components;

/// <summary>Identifies the player associated with an entity.</summary>
public record struct Player
{
    public uint EnetPeerId { get; set; }

    /// <summary>The player's identifier.</summary>
    public byte PlayerID { get; set; }

    /// <summary>The player's current game state.</summary>
    public PlayerState State { get; set; }
}

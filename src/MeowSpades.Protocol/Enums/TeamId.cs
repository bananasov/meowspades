namespace MeowSpades.Protocol.Enums;

/// <summary>Identifies a team, including spectator and neutral slots.</summary>
public enum TeamId : byte
{
    /// <summary>The first (blue) team.</summary>
    Blue = 0,

    /// <summary>The second (green) team.</summary>
    Green = 1,

    /// <summary>The neutral team.</summary>
    Neutral = 2,

    /// <summary>Spectators.</summary>
    Spectator = byte.MaxValue,
}

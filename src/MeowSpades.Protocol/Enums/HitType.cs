namespace MeowSpades.Protocol.Enums;

/// <summary>Identifies the body region or attack involved in a hit packet.</summary>
public enum HitType : byte
{
    /// <summary>The torso was hit.</summary>
    Torso = 0,

    /// <summary>The head was hit.</summary>
    Head = 1,

    /// <summary>The arms were hit.</summary>
    Arms = 2,

    /// <summary>The legs were hit.</summary>
    Legs = 3,

    /// <summary>The hit was a melee attack.</summary>
    Melee = 4,
}

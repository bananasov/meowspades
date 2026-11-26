namespace MeowSpades.Protocol.Enums;

/// <summary>Identifies the cause of a player's death.</summary>
public enum KillType : byte
{
    /// <summary>A weapon hit to the body or limbs.</summary>
    Weapon = 0,

    /// <summary>A headshot.</summary>
    Headshot = 1,

    /// <summary>A melee attack.</summary>
    Melee = 2,

    /// <summary>A grenade.</summary>
    Grenade = 3,

    /// <summary>Fall damage.</summary>
    Fall = 4,

    /// <summary>A team change.</summary>
    TeamChange = 5,

    /// <summary>A class change.</summary>
    ClassChange = 6,
}

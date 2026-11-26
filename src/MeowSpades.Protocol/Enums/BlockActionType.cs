namespace MeowSpades.Protocol.Enums;

/// <summary>Identifies a block action.</summary>
public enum BlockActionType : byte
{
    /// <summary>Places a block.</summary>
    Build = 0,

    /// <summary>Destroys a block with a bullet or left-button spade action.</summary>
    BulletOrSpadeDestroy = 1,

    /// <summary>Destroys blocks with a right-button spade action.</summary>
    SpadeDestroy = 2,

    /// <summary>Destroys blocks with a grenade.</summary>
    GrenadeDestroy = 3,
}

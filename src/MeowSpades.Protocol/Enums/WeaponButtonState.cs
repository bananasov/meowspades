namespace MeowSpades.Protocol.Enums;

/// <summary>Specifies the weapon buttons encoded in a weapon input packet.</summary>
[Flags]
public enum WeaponButtonState : byte
{
    /// <summary>No weapon buttons are pressed.</summary>
    None = 0,

    /// <summary>The primary-fire button is pressed.</summary>
    PrimaryFire = 1 << 0,

    /// <summary>The secondary-fire button is pressed.</summary>
    SecondaryFire = 1 << 1,
}

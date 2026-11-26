namespace MeowSpades.Protocol.Enums;

/// <summary>Specifies the movement keys encoded in an input packet.</summary>
[Flags]
public enum InputKeyState : byte
{
    /// <summary>No movement keys are pressed.</summary>
    None = 0,

    /// <summary>The up key is pressed.</summary>
    Up = 1 << 0,

    /// <summary>The down key is pressed.</summary>
    Down = 1 << 1,

    /// <summary>The left key is pressed.</summary>
    Left = 1 << 2,

    /// <summary>The right key is pressed.</summary>
    Right = 1 << 3,

    /// <summary>The jump key is pressed.</summary>
    Jump = 1 << 4,

    /// <summary>The crouch key is pressed.</summary>
    Crouch = 1 << 5,

    /// <summary>The sneak key is pressed.</summary>
    Sneak = 1 << 6,

    /// <summary>The sprint key is pressed.</summary>
    Sprint = 1 << 7,
}

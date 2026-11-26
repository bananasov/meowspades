namespace MeowSpades.Protocol.Enums;

/// <summary>Identifies an equipped tool.</summary>
public enum ToolType : byte
{
    /// <summary>The spade.</summary>
    Spade = 0,

    /// <summary>The block-building tool.</summary>
    Block = 1,

    /// <summary>The firearm.</summary>
    Gun = 2,

    /// <summary>The grenade.</summary>
    Grenade = 3,
}

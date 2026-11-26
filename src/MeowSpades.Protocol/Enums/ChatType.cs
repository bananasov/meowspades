namespace MeowSpades.Protocol.Enums;

/// <summary>Identifies a chat message destination.</summary>
public enum ChatType : byte
{
    /// <summary>Public chat.</summary>
    All = 0,

    /// <summary>Team-only chat.</summary>
    Team = 1,

    /// <summary>A system message.</summary>
    System = 2,
}

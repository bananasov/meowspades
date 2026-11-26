using System.Numerics;

namespace MeowSpades.Protocol.State;

/// <summary>Contains the Capture the Flag state appended to state data.</summary>
/// <param name="Team1Score">The first team's score.</param>
/// <param name="Team2Score">The second team's score.</param>
/// <param name="CaptureLimit">The score required to win.</param>
/// <param name="IntelFlags">Flags identifying which team carries enemy intel.</param>
/// <param name="Team1Intel">The first team's intel location.</param>
/// <param name="Team2Intel">The second team's intel location.</param>
/// <param name="Team1Base">The first team's base.</param>
/// <param name="Team2Base">The second team's base.</param>
public readonly record struct CaptureTheFlagState(
    byte Team1Score,
    byte Team2Score,
    byte CaptureLimit,
    byte IntelFlags,
    IntelLocationData Team1Intel,
    IntelLocationData Team2Intel,
    Vector3 Team1Base,
    Vector3 Team2Base
);

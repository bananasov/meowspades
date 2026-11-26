using System.Numerics;

namespace MeowSpades.Protocol.State;

/// <summary>Represents an intel location in Capture the Flag state.</summary>
/// <param name="IsHeld">Whether a player holds the intel.</param>
/// <param name="HoldingPlayerId">The holding player's identifier when held.</param>
/// <param name="GroundPosition">The position when the intel is on the ground.</param>
public readonly record struct IntelLocationData(
    bool IsHeld,
    byte HoldingPlayerId,
    Vector3 GroundPosition
);

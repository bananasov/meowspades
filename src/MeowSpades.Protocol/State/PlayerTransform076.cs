using System.Numerics;

namespace MeowSpades.Protocol.State;

/// <summary>Represents a protocol 0.76 player transform and its player identifier.</summary>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="Position">The player's position.</param>
/// <param name="Orientation">The player's orientation.</param>
public readonly record struct PlayerTransform076(
    byte PlayerId,
    Vector3 Position,
    Vector3 Orientation
);

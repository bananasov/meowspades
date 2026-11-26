using System.Numerics;

namespace MeowSpades.Protocol.State;

/// <summary>Represents a player's position and orientation in a world update.</summary>
/// <param name="Position">The player's position.</param><param name="Orientation">The player's orientation.</param>
public readonly record struct PlayerTransform(Vector3 Position, Vector3 Orientation);

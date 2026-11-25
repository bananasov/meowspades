using System.Numerics;

namespace MeowSpades.Components;

/// <summary>Describes an entity's position and orientation in 3D space.</summary>
/// <param name="Position">The entity's position in 3D space.</param>
/// <param name="Orientation">The entity's orientation in 3D space.</param>
public record struct Transform(
    Vector3 Position,
    Vector3 Orientation
);

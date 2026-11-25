using System.Numerics;

namespace MeowSpades.Components;

public record struct Transform(
    Vector3 Position,
    Vector3 Orientation
);

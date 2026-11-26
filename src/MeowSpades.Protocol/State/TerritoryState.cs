using System.Numerics;

namespace MeowSpades.Protocol.State;

/// <summary>Represents one Territory Control command post.</summary>
/// <param name="Position">The territory position.</param>
/// <param name="Team">The owning team identifier.</param>
public readonly record struct TerritoryState(Vector3 Position, byte Team);

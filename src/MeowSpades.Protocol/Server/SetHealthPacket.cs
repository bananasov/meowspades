namespace MeowSpades.Protocol.Server;

/// <summary>Sets health and identifies the source of damage.</summary>
/// <param name="Health">The resulting health value.</param>
/// <param name="DamageType">Zero for fall damage or one for weapon damage.</param>
/// <param name="SourceX">The source X coordinate.</param>
/// <param name="SourceY">The source Y coordinate.</param>
/// <param name="SourceZ">The source Z coordinate.</param>
public readonly record struct SetHealthPacket(byte Health, byte DamageType, float SourceX, float SourceY, float SourceZ) : IPacket
{
    /// <inheritdoc />
    public static byte Id => 5;
}

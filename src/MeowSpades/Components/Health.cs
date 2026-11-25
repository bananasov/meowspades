namespace MeowSpades.Components;

/// <summary>Tracks an entity's current and maximum health.</summary>
/// <param name="CurrentHealth">The entity's current health.</param>
/// <param name="MaxHealth">The entity's maximum health.</param>
public record struct Health(int CurrentHealth, int MaxHealth);

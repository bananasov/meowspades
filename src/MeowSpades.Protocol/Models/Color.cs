namespace MeowSpades.Protocol.Models;

/// <summary>Represents an 8-bit-per-channel color in protocol Blue, Green, Red order.</summary>
/// <param name="Blue">The blue channel.</param>
/// <param name="Green">The green channel.</param>
/// <param name="Red">The red channel.</param>
public readonly record struct Color(byte Blue, byte Green, byte Red);

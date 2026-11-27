namespace MeowSpades.Vxl.Models;

/// <summary>Represents an RGB voxel color.</summary>
/// <param name="Red">The red channel.</param>
/// <param name="Green">The green channel.</param>
/// <param name="Blue">The blue channel.</param>
public readonly record struct VoxelColor(byte Red, byte Green, byte Blue)
{
    /// <summary>The number of bytes used to store the RGB channels.</summary>
    public const int BytesPerColor = 3;
}

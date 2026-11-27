namespace MeowSpades.Vxl.Models;

internal readonly record struct VoxelSpanHeader(
    byte SpanLength,
    byte TopStart,
    byte TopEnd,
    byte AirStart);

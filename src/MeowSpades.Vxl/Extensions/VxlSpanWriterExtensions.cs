using MeowSpades.Binary;
using MeowSpades.Vxl.Models;

namespace MeowSpades.Vxl.Extensions;

internal static class VxlSpanWriterExtensions
{
    /// <summary>Writes the VXL span header in its four-byte little-endian field order.</summary>
    /// <param name="writer">The writer to append the header to.</param>
    /// <param name="header">The VXL header fields.</param>
    internal static void WriteHeader(this ref SpanWriter writer, VoxelSpanHeader header)
    {
        uint packedHeader =
            header.SpanLength |
            ((uint)header.TopStart << 8) |
            ((uint)header.TopEnd << 16) |
            ((uint)header.AirStart << 24);
        writer.WriteUInt32(packedHeader);
    }
}

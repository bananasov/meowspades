using MeowSpades.Binary;
using MeowSpades.Protocol.Models;

namespace MeowSpades.Protocol.IO;

internal static class ColorBinaryExtensions
{
    /// <summary>Reads a color encoded as blue, green, then red bytes.</summary>
    /// <param name="reader">The reader to consume bytes from.</param>
    /// <returns>The color read.</returns>
    internal static Color ReadColor(this ref SpanReader reader) =>
        new(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());

    /// <summary>Writes a color as blue, green, then red bytes.</summary>
    /// <param name="writer">The writer to write bytes to.</param>
    /// <param name="value">The color to write.</param>
    internal static void WriteColor(this ref SpanWriter writer, Color value)
    {
        writer.WriteByte(value.Blue);
        writer.WriteByte(value.Green);
        writer.WriteByte(value.Red);
    }
}

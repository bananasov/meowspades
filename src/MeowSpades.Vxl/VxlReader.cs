using MeowSpades.Binary;
using MeowSpades.Vxl.Models;

namespace MeowSpades.Vxl;

/// <summary>Decodes VXL map data.</summary>
public static class VxlReader
{
    private const int SpanHeaderSize = 4;
    private const int ColorRecordSize = 4;
    private const byte DefaultInteriorRed = 40;
    private const byte DefaultInteriorGreen = 64;
    private const byte DefaultInteriorBlue = 103;

    /// <summary>Decodes all columns from a VXL payload.</summary>
    /// <param name="source">The VXL payload.</param>
    /// <returns>The decoded voxel map.</returns>
    /// <exception cref="InvalidDataException">The payload is truncated or contains invalid span data.</exception>
    public static VoxelMap Read(ReadOnlySpan<byte> source)
    {
        var map = new VoxelMap();
        var reader = new SpanReader(source);
        Span<ulong> solidColumns = map.SolidColumns;
        Span<byte> colorData = map.ColorData;

        for (int column = 0; column < VoxelMap.ColumnCount; column++)
        {
            while (true)
            {
                if (reader.Remaining < SpanHeaderSize)
                {
                    throw InvalidData(column, "truncated span header");
                }

                int spanLength = reader.ReadByte();
                int topStart = reader.ReadByte();
                int topEnd = reader.ReadByte();
                _ = reader.ReadByte(); // air_start is consumed by the preceding span.

                if (topEnd >= VoxelMap.SizeZ || topStart > topEnd + 1)
                {
                    throw InvalidData(column, "invalid top range");
                }

                int topCount = topEnd - topStart + 1;
                if (spanLength == 0)
                {
                    int topBytes = topCount * ColorRecordSize;
                    if (reader.Remaining < topBytes)
                    {
                        throw InvalidData(column, "truncated final span colors");
                    }

                    ReadOnlySpan<byte> finalTopColors = reader.ReadBytes(topBytes);
                    solidColumns[column] |= CreateMask(topStart, topCount);
                    CopyColors(colorData, column, topStart, finalTopColors, topCount);

                    int finalFillStart = topStart + topCount;
                    int fillCount = VoxelMap.SizeZ - finalFillStart;
                    solidColumns[column] |= CreateMask(finalFillStart, fillCount);
                    FillInteriorColors(colorData, column, finalFillStart, fillCount);
                    break;
                }

                if (spanLength > VoxelMap.SizeZ + 1 || topCount > spanLength - 1)
                {
                    throw InvalidData(column, "invalid span length");
                }

                int bodyBytes = spanLength * ColorRecordSize;
                if (reader.Remaining < bodyBytes)
                {
                    throw InvalidData(column, "truncated span body or following header");
                }

                int bottomCount = spanLength - 1 - topCount;
                int nextHeaderOffset = (topCount + bottomCount) * ColorRecordSize;
                int nextAirStart = reader.RemainingSpan[nextHeaderOffset + 3];
                int bottomStart = nextAirStart - bottomCount;
                int fillStart = topStart + topCount;

                if (nextAirStart > VoxelMap.SizeZ || bottomStart < 0 || fillStart > bottomStart)
                {
                    throw InvalidData(column, "invalid bottom range");
                }

                ReadOnlySpan<byte> topColors = reader.ReadBytes(topCount * ColorRecordSize);
                ReadOnlySpan<byte> bottomColors = reader.ReadBytes(bottomCount * ColorRecordSize);

                solidColumns[column] |= CreateMask(topStart, topCount);
                int solidFillCount = nextAirStart - fillStart;
                solidColumns[column] |= CreateMask(fillStart, solidFillCount);
                FillInteriorColors(colorData, column, fillStart, bottomStart - fillStart);
                CopyColors(colorData, column, topStart, topColors, topCount);
                CopyColors(colorData, column, bottomStart, bottomColors, bottomCount);
            }
        }

        return map;
    }

    private static void CopyColors(
        Span<byte> colorData,
        int column,
        int startZ,
        ReadOnlySpan<byte> records,
        int count)
    {
        int voxelOffset = (column * VoxelMap.SizeZ + startZ) * VoxelColor.BytesPerColor;
        for (int i = 0; i < count; i++)
        {
            int recordOffset = i * ColorRecordSize;
            colorData[voxelOffset] = records[recordOffset];
            colorData[voxelOffset + 1] = records[recordOffset + 1];
            colorData[voxelOffset + 2] = records[recordOffset + 2];
            voxelOffset += VoxelColor.BytesPerColor;
        }
    }

    private static void FillInteriorColors(Span<byte> colorData, int column, int startZ, int count)
    {
        int voxelOffset = (column * VoxelMap.SizeZ + startZ) * VoxelColor.BytesPerColor;
        int end = voxelOffset + count * VoxelColor.BytesPerColor;
        for (; voxelOffset < end; voxelOffset += VoxelColor.BytesPerColor)
        {
            colorData[voxelOffset] = DefaultInteriorRed;
            colorData[voxelOffset + 1] = DefaultInteriorGreen;
            colorData[voxelOffset + 2] = DefaultInteriorBlue;
        }
    }

    private static ulong CreateMask(int start, int count)
    {
        if (count == 0)
        {
            return 0;
        }

        ulong mask = count == VoxelMap.SizeZ
            ? ulong.MaxValue
            : (1UL << count) - 1;
        return mask << start;
    }

    private static InvalidDataException InvalidData(int column, string reason) =>
        new($"Invalid VXL data at column {column}: {reason}.");
}

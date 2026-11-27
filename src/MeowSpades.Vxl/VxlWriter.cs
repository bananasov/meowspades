using System.Numerics;
using MeowSpades.Binary;
using MeowSpades.Vxl.Extensions;
using MeowSpades.Vxl.Models;

namespace MeowSpades.Vxl;

/// <summary>Encodes voxel maps as VXL data.</summary>
public static class VxlWriter
{
    private const int ColorRecordSize = 4;

    /// <summary>Gets the maximum encoded size for the specified number of columns.</summary>
    /// <param name="columnCount">The number of consecutive columns.</param>
    /// <returns>The maximum number of output bytes required.</returns>
    public static int GetMaxSize(int columnCount = VoxelMap.ColumnCount)
    {
        if ((uint)columnCount > VoxelMap.ColumnCount)
        {
            throw new ArgumentOutOfRangeException(nameof(columnCount));
        }

        return checked(columnCount * (VoxelMap.SizeZ + 1) * ColorRecordSize);
    }

    /// <summary>Encodes consecutive columns into a caller-provided destination.</summary>
    /// <param name="map">The map to encode.</param>
    /// <param name="destination">The output buffer.</param>
    /// <param name="startColumn">The first linear column index.</param>
    /// <param name="columnCount">The number of columns to encode.</param>
    /// <returns>The number of bytes written.</returns>
    public static int Write(
        VoxelMap map,
        Span<byte> destination,
        int startColumn = 0,
        int columnCount = VoxelMap.ColumnCount)
    {
        ArgumentNullException.ThrowIfNull(map);
        ValidateColumnRange(startColumn, columnCount);

        var writer = new SpanWriter(destination);
        Span<byte> colorRecords = stackalloc byte[VoxelMap.SizeZ * ColorRecordSize];
        Span<ulong> solidColumns = map.SolidColumns;
        Span<byte> colorData = map.ColorData;

        int endColumn = startColumn + columnCount;
        for (int column = startColumn; column < endColumn; column++)
        {
            WriteColumn(ref writer, solidColumns, colorData, column, colorRecords);
        }

        return writer.WrittenCount;
    }

    private static void WriteColumn(
        scoped ref SpanWriter writer,
        scoped Span<ulong> solidColumns,
        scoped Span<byte> colorData,
        int column,
        scoped Span<byte> colorRecords)
    {
        ulong solid = solidColumns[column];
        ulong surrounded = GetSurrounded(solidColumns, column);

        // Surface voxels are stored with their colors; fully surrounded voxels are omitted and reconstructed as dirt.
        ulong colored = solid & ~surrounded;
        ulong filled = solid & surrounded;
        int nextAirStart = 0;

        while (true)
        {
            int airStart = nextAirStart;
            int topStart = BitOperations.TrailingZeroCount(solid);

            // Mask completed lower z values so the trailing-zero scans find this span's next surface/interior boundary.
            colored |= MaskBelow(topStart);
            int filledStart = BitOperations.TrailingZeroCount(~colored);
            int topEnd = filledStart - 1;

            filled |= MaskBelow(filledStart);
            int bottomStart = BitOperations.TrailingZeroCount(~filled);

            if (bottomStart == VoxelMap.SizeZ)
            {
                // A zero-length final header terminates the column
                writer.WriteHeader(new(
                    SpanLength: 0,
                    TopStart: (byte)topStart,
                    TopEnd: (byte)topEnd,
                    AirStart: (byte)airStart));
                WriteColorRecords(ref writer, colorData, column, topStart, topEnd, colorRecords);
                return;
            }

            // The next header's air_start delimits this span's bottom colors and any omitted interior fill.
            colored |= MaskBelow(bottomStart);
            nextAirStart = BitOperations.TrailingZeroCount(~colored);

            int topCount = filledStart - topStart;
            int bottomCount = nextAirStart - bottomStart;
            int spanLength = 1 + topCount + bottomCount;
            if (spanLength > VoxelMap.SizeZ + 1 || nextAirStart <= topStart)
            {
                throw new InvalidOperationException("The VXL span encoder produced an invalid span.");
            }

            writer.WriteHeader(new(
                SpanLength: (byte)spanLength,
                TopStart: (byte)topStart,
                TopEnd: (byte)topEnd,
                AirStart: (byte)airStart));
            WriteColorRecords(ref writer, colorData, column, topStart, topEnd, colorRecords);
            WriteColorRecords(ref writer, colorData, column, bottomStart, nextAirStart - 1, colorRecords);

            solid &= ~MaskBelow(nextAirStart);
        }
    }

    private static ulong GetSurrounded(Span<ulong> columns, int column)
    {
        int x = column % VoxelMap.SizeX;
        ulong left = x > 0 ? columns[column - 1] : ulong.MaxValue;
        ulong right = x < VoxelMap.SizeX - 1 ? columns[column + 1] : ulong.MaxValue;
        ulong previousRow = column >= VoxelMap.SizeX
            ? columns[column - VoxelMap.SizeX]
            : ulong.MaxValue;
        ulong nextRow = column < VoxelMap.ColumnCount - VoxelMap.SizeX
            ? columns[column + VoxelMap.SizeX]
            : ulong.MaxValue;

        ulong horizontal = left & right & previousRow & nextRow;
        ulong vertical = ((columns[column] >> 1) | (1UL << (VoxelMap.SizeZ - 1))) &
            (columns[column] << 1);
        return horizontal & vertical;
    }


    private static void WriteColorRecords(
        scoped ref SpanWriter writer,
        scoped Span<byte> colorData,
        int column,
        int startZ,
        int endZ,
        scoped Span<byte> records)
    {
        int count = endZ - startZ + 1;
        if (count <= 0)
        {
            return;
        }

        int colorOffset = (column * VoxelMap.SizeZ + startZ) * VoxelColor.BytesPerColor;
        for (int i = 0; i < count; i++)
        {
            int recordOffset = i * ColorRecordSize;
            records[recordOffset] = colorData[colorOffset];
            records[recordOffset + 1] = colorData[colorOffset + 1];
            records[recordOffset + 2] = colorData[colorOffset + 2];
            colorOffset += VoxelColor.BytesPerColor;
            records[recordOffset + 3] = 0;
        }

        writer.WriteBytes(records[..(count * ColorRecordSize)]);
    }

    private static ulong MaskBelow(int bitCount) => bitCount switch
    {
        0 => 0,
        VoxelMap.SizeZ => ulong.MaxValue,
        _ => (1UL << bitCount) - 1
    };


    private static void ValidateColumnRange(int startColumn, int columnCount)
    {
        if ((uint)startColumn > VoxelMap.ColumnCount)
        {
            throw new ArgumentOutOfRangeException(nameof(startColumn));
        }

        if ((uint)columnCount > VoxelMap.ColumnCount - startColumn)
        {
            throw new ArgumentOutOfRangeException(nameof(columnCount));
        }
    }
}

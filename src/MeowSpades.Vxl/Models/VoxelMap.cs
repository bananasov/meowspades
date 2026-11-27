namespace MeowSpades.Vxl.Models;

/// <summary>Stores solidity and RGB colors for a 512 × 512 × 64 voxel map.</summary>
public sealed class VoxelMap
{
    public const int SizeX = 512;
    public const int SizeY = 512;
    public const int SizeZ = 64;
    public const int ColumnCount = SizeX * SizeY;
    public const int VoxelCount = ColumnCount * SizeZ;

    private readonly ulong[] _solidColumns = new ulong[ColumnCount];
    private readonly byte[] _colorData = new byte[VoxelCount * VoxelColor.BytesPerColor];

    /// <summary>Gets whether the voxel at the specified coordinates is solid.</summary>
    public bool IsSolid(int x, int y, int z)
    {
        int column = GetColumnIndex(x, y);
        ValidateZ(z);
        return (_solidColumns[column] & (1UL << z)) != 0;
    }

    /// <summary>Sets whether the voxel at the specified coordinates is solid.</summary>
    public void SetSolid(int x, int y, int z, bool solid)
    {
        int column = GetColumnIndex(x, y);
        ValidateZ(z);
        ulong mask = 1UL << z;
        _solidColumns[column] = solid
            ? _solidColumns[column] | mask
            : _solidColumns[column] & ~mask;
    }

    /// <summary>Gets the solidity bitmask for a column, with bit zero representing Z zero.</summary>
    public ulong GetSolidColumn(int x, int y) => _solidColumns[GetColumnIndex(x, y)];

    /// <summary>Replaces the solidity bitmask for a column.</summary>
    public void SetSolidColumn(int x, int y, ulong solidity) =>
        _solidColumns[GetColumnIndex(x, y)] = solidity;

    /// <summary>Gets the RGB color at the specified coordinates.</summary>
    public VoxelColor GetColor(int x, int y, int z)
    {
        int offset = GetColorOffset(x, y, z);
        return new VoxelColor(
            _colorData[offset],
            _colorData[offset + 1],
            _colorData[offset + 2]);
    }

    /// <summary>Sets the RGB color at the specified coordinates.</summary>
    public void SetColor(int x, int y, int z, VoxelColor color)
    {
        int offset = GetColorOffset(x, y, z);
        _colorData[offset] = color.Red;
        _colorData[offset + 1] = color.Green;
        _colorData[offset + 2] = color.Blue;
    }

    internal Span<ulong> SolidColumns => _solidColumns;
    internal Span<byte> ColorData => _colorData;

    private static int GetColumnIndex(int x, int y)
    {
        if ((uint)x >= SizeX)
        {
            throw new ArgumentOutOfRangeException(nameof(x));
        }

        if ((uint)y >= SizeY)
        {
            throw new ArgumentOutOfRangeException(nameof(y));
        }

        return y * SizeX + x;
    }

    private static int GetColorOffset(int x, int y, int z)
    {
        int column = GetColumnIndex(x, y);
        ValidateZ(z);
        return (column * SizeZ + z) * VoxelColor.BytesPerColor;
    }

    private static void ValidateZ(int z)
    {
        if ((uint)z >= SizeZ)
        {
            throw new ArgumentOutOfRangeException(nameof(z));
        }
    }
}

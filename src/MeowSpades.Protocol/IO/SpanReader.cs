using System.Buffers.Binary;

namespace MeowSpades.Protocol.IO;

/// <summary>Reads little-endian protocol values from a caller-provided buffer.</summary>
internal ref struct SpanReader
{
    private readonly ReadOnlySpan<byte> _source;
    private int _position;

    /// <summary>Initializes a reader over serialized packet data.</summary>
    /// <param name="source">The serialized data to read.</param>
    internal SpanReader(ReadOnlySpan<byte> source)
    {
        _source = source;
        _position = 0;
    }

    /// <summary>Gets the number of bytes consumed from the source.</summary>
    internal readonly int ConsumedCount => _position;

    /// <summary>Gets the number of bytes remaining in the source.</summary>
    internal readonly int Remaining => _source.Length - _position;

    /// <summary>Gets the portion of the source that has not been consumed.</summary>
    internal readonly ReadOnlySpan<byte> RemainingSpan => _source[_position..];

    /// <summary>Reads an unsigned 8-bit value.</summary>
    /// <returns>The value read.</returns>
    internal byte ReadByte()
    {
        EnsureAvailable(sizeof(byte));
        return _source[_position++];
    }

    /// <summary>Reads a signed 8-bit value.</summary>
    /// <returns>The value read.</returns>
    internal sbyte ReadSByte() => unchecked((sbyte)ReadByte());

    /// <summary>Reads a Boolean value, interpreting any nonzero byte as <see langword="true"/>.</summary>
    /// <returns>The value read.</returns>
    internal bool ReadBoolean() => ReadByte() != 0;

    /// <summary>Reads a signed 16-bit little-endian value.</summary>
    /// <returns>The value read.</returns>
    internal short ReadInt16()
    {
        EnsureAvailable(sizeof(short));
        short value = BinaryPrimitives.ReadInt16LittleEndian(_source[_position..]);
        _position += sizeof(short);
        return value;
    }

    /// <summary>Reads an unsigned 16-bit little-endian value.</summary>
    /// <returns>The value read.</returns>
    internal ushort ReadUInt16()
    {
        EnsureAvailable(sizeof(ushort));
        ushort value = BinaryPrimitives.ReadUInt16LittleEndian(_source[_position..]);
        _position += sizeof(ushort);
        return value;
    }

    /// <summary>Reads a signed 32-bit little-endian value.</summary>
    /// <returns>The value read.</returns>
    internal int ReadInt32()
    {
        EnsureAvailable(sizeof(int));
        int value = BinaryPrimitives.ReadInt32LittleEndian(_source[_position..]);
        _position += sizeof(int);
        return value;
    }

    /// <summary>Reads an unsigned 32-bit little-endian value.</summary>
    /// <returns>The value read.</returns>
    internal uint ReadUInt32()
    {
        EnsureAvailable(sizeof(uint));
        uint value = BinaryPrimitives.ReadUInt32LittleEndian(_source[_position..]);
        _position += sizeof(uint);
        return value;
    }

    /// <summary>Reads a signed 64-bit little-endian value.</summary>
    /// <returns>The value read.</returns>
    internal long ReadInt64()
    {
        EnsureAvailable(sizeof(long));
        long value = BinaryPrimitives.ReadInt64LittleEndian(_source[_position..]);
        _position += sizeof(long);
        return value;
    }

    /// <summary>Reads an unsigned 64-bit little-endian value.</summary>
    /// <returns>The value read.</returns>
    internal ulong ReadUInt64()
    {
        EnsureAvailable(sizeof(ulong));
        ulong value = BinaryPrimitives.ReadUInt64LittleEndian(_source[_position..]);
        _position += sizeof(ulong);
        return value;
    }

    /// <summary>Reads a single-precision floating-point value in little-endian format.</summary>
    /// <returns>The value read.</returns>
    internal float ReadFloat() => BitConverter.Int32BitsToSingle(ReadInt32());

    /// <summary>Reads a double-precision floating-point value in little-endian format.</summary>
    /// <returns>The value read.</returns>
    internal double ReadDouble() => BitConverter.Int64BitsToDouble(ReadInt64());

    /// <summary>Reads a specified number of bytes from the source.</summary>
    /// <param name="count">The number of bytes to read.</param>
    /// <returns>A slice of the source containing the bytes read.</returns>
    internal ReadOnlySpan<byte> ReadBytes(int count)
    {
        EnsureAvailable(count);
        ReadOnlySpan<byte> value = _source.Slice(_position, count);
        _position += count;
        return value;
    }

    private readonly void EnsureAvailable(int count)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(count);

        if (count > Remaining)
        {
            throw new EndOfStreamException("The source does not contain enough data.");
        }
    }
}

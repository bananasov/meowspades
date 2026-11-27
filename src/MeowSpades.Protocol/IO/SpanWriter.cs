using System.Buffers.Binary;
using System.Numerics;
using MeowSpades.Protocol.Models;

namespace MeowSpades.Protocol.IO;

/// <summary>Writes little-endian protocol values to a caller-provided buffer.</summary>
internal ref struct SpanWriter
{
    private readonly Span<byte> _destination;
    private int _position;

    /// <summary>Initializes a writer over a destination buffer.</summary>
    /// <param name="destination">The buffer to write to.</param>
    internal SpanWriter(Span<byte> destination)
    {
        _destination = destination;
        _position = 0;
    }

    /// <summary>Gets the number of bytes written to the destination.</summary>
    internal readonly int WrittenCount => _position;

    /// <summary>Gets the number of bytes remaining in the destination.</summary>
    internal readonly int Remaining => _destination.Length - _position;

    /// <summary>Gets the portion of the destination already written.</summary>
    internal readonly Span<byte> WrittenSpan => _destination[.._position];

    /// <summary>Writes an unsigned 8-bit value.</summary>
    /// <param name="value">The value to write.</param>
    internal void WriteByte(byte value)
    {
        EnsureCapacity(sizeof(byte));
        _destination[_position++] = value;
    }

    /// <summary>Writes a signed 8-bit value.</summary>
    /// <param name="value">The value to write.</param>
    internal void WriteSByte(sbyte value) => WriteByte(unchecked((byte)value));

    /// <summary>Writes a Boolean value as zero or one.</summary>
    /// <param name="value">The value to write.</param>
    internal void WriteBoolean(bool value) => WriteByte(value ? (byte)1 : (byte)0);

    /// <summary>Writes a signed 16-bit little-endian value.</summary>
    /// <param name="value">The value to write.</param>
    internal void WriteInt16(short value)
    {
        EnsureCapacity(sizeof(short));
        BinaryPrimitives.WriteInt16LittleEndian(_destination[_position..], value);
        _position += sizeof(short);
    }

    /// <summary>Writes an unsigned 16-bit little-endian value.</summary>
    /// <param name="value">The value to write.</param>
    internal void WriteUInt16(ushort value)
    {
        EnsureCapacity(sizeof(ushort));
        BinaryPrimitives.WriteUInt16LittleEndian(_destination[_position..], value);
        _position += sizeof(ushort);
    }

    /// <summary>Writes a signed 32-bit little-endian value.</summary>
    /// <param name="value">The value to write.</param>
    internal void WriteInt32(int value)
    {
        EnsureCapacity(sizeof(int));
        BinaryPrimitives.WriteInt32LittleEndian(_destination[_position..], value);
        _position += sizeof(int);
    }

    /// <summary>Writes an unsigned 32-bit little-endian value.</summary>
    /// <param name="value">The value to write.</param>
    internal void WriteUInt32(uint value)
    {
        EnsureCapacity(sizeof(uint));
        BinaryPrimitives.WriteUInt32LittleEndian(_destination[_position..], value);
        _position += sizeof(uint);
    }

    /// <summary>Writes a signed 64-bit little-endian value.</summary>
    /// <param name="value">The value to write.</param>
    internal void WriteInt64(long value)
    {
        EnsureCapacity(sizeof(long));
        BinaryPrimitives.WriteInt64LittleEndian(_destination[_position..], value);
        _position += sizeof(long);
    }

    /// <summary>Writes an unsigned 64-bit little-endian value.</summary>
    /// <param name="value">The value to write.</param>
    internal void WriteUInt64(ulong value)
    {
        EnsureCapacity(sizeof(ulong));
        BinaryPrimitives.WriteUInt64LittleEndian(_destination[_position..], value);
        _position += sizeof(ulong);
    }

    /// <summary>Writes a single-precision floating-point value in little-endian format.</summary>
    /// <param name="value">The value to write.</param>
    internal void WriteFloat(float value) => WriteInt32(BitConverter.SingleToInt32Bits(value));

    /// <summary>Writes a double-precision floating-point value in little-endian format.</summary>
    /// <param name="value">The value to write.</param>
    internal void WriteDouble(double value) => WriteInt64(BitConverter.DoubleToInt64Bits(value));

    /// <summary>Writes the X, Y, and Z components of a vector in order.</summary>
    /// <param name="value">The vector to write.</param>
    internal void WriteVector3(Vector3 value) => WriteVector3(value.X, value.Y, value.Z);

    /// <summary>Writes X, Y, and Z components as little-endian single-precision values.</summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    /// <param name="z">The Z component.</param>
    internal void WriteVector3(float x, float y, float z)
    {
        WriteFloat(x);
        WriteFloat(y);
        WriteFloat(z);
    }

    /// <summary>Writes X, Y, and Z components as little-endian signed 32-bit integers.</summary>
    /// <param name="value">The vector to write.</param>
    internal void WriteInt32Vector3(Vector3 value)
    {
        WriteInt32((int)value.X);
        WriteInt32((int)value.Y);
        WriteInt32((int)value.Z);
    }

    /// <summary>Writes a color as blue, green, then red bytes.</summary>
    /// <param name="value">The color to write.</param>
    internal void WriteColor(Color value)
    {
        WriteByte(value.Blue);
        WriteByte(value.Green);
        WriteByte(value.Red);
    }

    /// <summary>Copies bytes into the destination.</summary>
    /// <param name="value">The bytes to write.</param>
    internal void WriteBytes(scoped ReadOnlySpan<byte> value)
    {
        EnsureCapacity(value.Length);
        value.CopyTo(_destination[_position..]);
        _position += value.Length;
    }

    private readonly void EnsureCapacity(int count)
    {
        if (count > Remaining)
        {
            throw new ArgumentException("The destination buffer does not have enough space.", "destination");
        }
    }
}

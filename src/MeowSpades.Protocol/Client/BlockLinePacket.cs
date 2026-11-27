using System.Numerics;
using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Client;

/// <summary>Creates a line of blocks between two map coordinates.</summary>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="StartLocation">The integer starting map coordinates.</param>
/// <param name="EndLocation">The integer ending map coordinates.</param>
public readonly record struct BlockLinePacket(
    byte PlayerId,
    Vector3 StartLocation,
    Vector3 EndLocation
) : IPacket<BlockLinePacket>
{
    /// <inheritdoc />
    public static byte Id => 14;

    /// <inheritdoc />
    public int GetMaxSize() => 26;

    /// <inheritdoc />
    public static BlockLinePacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 26;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var playerId = reader.ReadByte();
        var startLocation = reader.ReadInt32Vector3();
        var endLocation = reader.ReadInt32Vector3();


        return new BlockLinePacket(playerId, startLocation, endLocation);
    }

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(PlayerId);

        writer.WriteInt32Vector3(StartLocation);
        writer.WriteInt32Vector3(EndLocation);

        return writer.WrittenCount;
    }
}

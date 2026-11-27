using System.Numerics;
using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Server;

/// <summary>Reports that a player dropped intel.</summary>
/// <param name="PlayerId">The player's identifier.</param>
/// <param name="Position">The intel's drop position.</param>
public readonly record struct IntelDropPacket(byte PlayerId, Vector3 Position) : IPacket<IntelDropPacket>
{
    /// <inheritdoc />
    public static byte Id => 25;

    /// <inheritdoc />
    public static IntelDropPacket Deserialize(ReadOnlySpan<byte> source)
    {
        const int SerializedSize = 14;
        PacketValidation.ThrowIfInvalidLength(source, SerializedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var playerId = reader.ReadByte();
        var position = reader.ReadVector3();

        return new IntelDropPacket(playerId, position);
    }

    /// <inheritdoc />
    public int GetMaxSize() => 14;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(PlayerId);
        writer.WriteVector3(Position);

        return writer.WrittenCount;
    }
}

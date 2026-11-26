using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Server;

/// <summary>Reports that a player dropped intel.</summary>
/// <param name="PlayerId">The player's identifier.</param>
/// <param name="X">The drop X coordinate.</param>
/// <param name="Y">The drop Y coordinate.</param>
/// <param name="Z">The drop Z coordinate.</param>
public readonly record struct IntelDropPacket(byte PlayerId, float X, float Y, float Z) : IPacket<IntelDropPacket>
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
        var x = reader.ReadFloat();
        var y = reader.ReadFloat();
        var z = reader.ReadFloat();

        return new IntelDropPacket(playerId, x, y, z);
    }

    /// <inheritdoc />
    public int GetMaxSize() => 14;

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(PlayerId);
        writer.WriteFloat(X);
        writer.WriteFloat(Y);
        writer.WriteFloat(Z);

        return writer.WrittenCount;
    }
}

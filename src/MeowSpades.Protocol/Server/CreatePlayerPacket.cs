using System.Text;
using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Server;

/// <summary>Creates or respawns a player.</summary>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="Weapon">The weapon identifier.</param>
/// <param name="Team">The team identifier.</param>
/// <param name="X">The spawn X coordinate.</param>
/// <param name="Y">The spawn Y coordinate.</param>
/// <param name="Z">The spawn Z coordinate.</param>
/// <param name="Name">The CP437-encoded player name as a .NET string.</param>
public readonly record struct CreatePlayerPacket(
    byte PlayerId,
    byte Weapon,
    byte Team,
    float X,
    float Y,
    float Z,
    string Name
) : IPacket<CreatePlayerPacket>
{
    private const int FixedSize = 16;
    private static readonly Encoding Cp437 = Encoding.GetEncoding(437);

    /// <inheritdoc />
    public static byte Id => 12;

    /// <inheritdoc />
    public int GetMaxSize() => FixedSize + Cp437.GetByteCount(Name);

    /// <inheritdoc />
    public static CreatePlayerPacket Deserialize(ReadOnlySpan<byte> source)
    {
        PacketValidation.ThrowIfTooShort(source, FixedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var playerId = reader.ReadByte();
        var weapon = reader.ReadByte();
        var team = reader.ReadByte();
        var x = reader.ReadFloat();
        var y = reader.ReadFloat();
        var z = reader.ReadFloat();

        var nameBytes = reader.ReadBytes(reader.Remaining);
        var name = Cp437.GetString(nameBytes);

        return new CreatePlayerPacket(playerId, weapon, team, x, y, z, name);
    }

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(PlayerId);
        writer.WriteByte(Weapon);
        writer.WriteByte(Team);
        writer.WriteFloat(X);
        writer.WriteFloat(Y);
        writer.WriteFloat(Z);
        writer.WriteBytes(Cp437.GetBytes(Name));

        return writer.WrittenCount;
    }
}

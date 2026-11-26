using System.Text;
using MeowSpades.Protocol.IO;

namespace MeowSpades.Protocol.Client;

/// <summary>Announces a player's team, equipment, statistics, color, and name.</summary>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="Team">The team identifier.</param>
/// <param name="Weapon">The weapon identifier.</param>
/// <param name="HeldItem">The held item identifier.</param>
/// <param name="Kills">The kill count.</param>
/// <param name="Blue">The blue component.</param>
/// <param name="Green">The green component.</param>
/// <param name="Red">The red component.</param>
/// <param name="Name">The CP437-encoded name as a .NET string.</param>
public readonly record struct ExistingPlayerPacket(
    byte PlayerId,
    byte Team,
    byte Weapon,
    byte HeldItem,
    uint Kills,
    byte Blue,
    byte Green,
    byte Red,
    string Name
) : IPacket<ExistingPlayerPacket>
{
    private const int FixedSize = 12;
    private static readonly Encoding Cp437 = Encoding.GetEncoding(437);

    /// <inheritdoc />
    public static byte Id => 9;

    /// <inheritdoc />
    public int GetMaxSize() => FixedSize + Cp437.GetByteCount(Name);

    /// <inheritdoc />
    public static ExistingPlayerPacket Deserialize(ReadOnlySpan<byte> source)
    {
        PacketValidation.ThrowIfTooShort(source, FixedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var playerId = reader.ReadByte();
        var team = reader.ReadByte();
        var weapon = reader.ReadByte();
        var heldItem = reader.ReadByte();
        var kills = reader.ReadUInt32();
        var blue = reader.ReadByte();
        var green = reader.ReadByte();
        var red = reader.ReadByte();

        var nameBytes = reader.ReadBytes(reader.Remaining);
        var name = Cp437.GetString(nameBytes);

        return new ExistingPlayerPacket(
            playerId,
            team,
            weapon,
            heldItem,
            kills,
            blue,
            green,
            red,
            name
        );
    }

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(PlayerId);
        writer.WriteByte(Team);
        writer.WriteByte(Weapon);
        writer.WriteByte(HeldItem);
        writer.WriteUInt32(Kills);
        writer.WriteByte(Blue);
        writer.WriteByte(Green);
        writer.WriteByte(Red);
        writer.WriteBytes(Cp437.GetBytes(Name));

        return writer.WrittenCount;
    }
}

using System.Text;
using MeowSpades.Binary;
using MeowSpades.Protocol.IO;
using MeowSpades.Protocol.Models;

namespace MeowSpades.Protocol.Client;

/// <summary>Announces a player's team, equipment, statistics, color, and name.</summary>
/// <param name="PlayerId">The player identifier.</param>
/// <param name="Team">The team identifier.</param>
/// <param name="Weapon">The weapon identifier.</param>
/// <param name="HeldItem">The held item identifier.</param>
/// <param name="Kills">The kill count.</param>
/// <param name="BlockColor">The color of the player's block.</param>
/// <param name="Name">The CP437-encoded name as a .NET string.</param>
public readonly record struct ExistingPlayerPacket(
    byte PlayerId,
    byte Team,
    byte Weapon,
    byte HeldItem,
    uint Kills,
    Color BlockColor,
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
        var blockColor = reader.ReadColor();

        var nameBytes = reader.ReadBytes(reader.Remaining);
        var name = Cp437.GetString(nameBytes);

        return new ExistingPlayerPacket(
            playerId,
            team,
            weapon,
            heldItem,
            kills,
            blockColor,
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
        writer.WriteColor(BlockColor);
        writer.WriteBytes(Cp437.GetBytes(Name));

        return writer.WrittenCount;
    }
}

using System.Numerics;
using System.Text;
using MeowSpades.Protocol.Enums;
using MeowSpades.Binary;
using MeowSpades.Protocol.IO;
using MeowSpades.Protocol.Models;

namespace MeowSpades.Protocol.State;

/// <summary>Contains game parameters and the mode-specific state sent after map transfer.</summary>
/// <param name="PlayerId">The local player's identifier.</param>
/// <param name="FogColor">The fog color.</param>
/// <param name="Team1Color">The first team's color.</param>
/// <param name="Team2Color">The second team's color.</param>
/// <param name="Team1Name">The first team's fixed-width CP437 name.</param>
/// <param name="Team2Name">The second team's fixed-width CP437 name.</param>
/// <param name="Mode">The game mode.</param>
/// <param name="CaptureTheFlag">The CTF state when appropriate.</param>
/// <param name="Territories">The TC territories when appropriate.</param>
public readonly record struct StateDataPacket(
    byte PlayerId,
    Color FogColor,
    Color Team1Color,
    Color Team2Color,
    string Team1Name,
    string Team2Name,
    GameMode Mode,
    CaptureTheFlagState? CaptureTheFlag,
    ReadOnlyMemory<TerritoryState> Territories
) : IPacket<StateDataPacket>
{
    private const int FixedSize = 32;
    private const int TeamNameSize = 10;
    private const int CaptureTheFlagStateSize = 52;
    private const int TerritorySize = 13;
    private const int MaximumTerritories = 16;

    private static readonly Encoding Cp437 = Encoding.GetEncoding(437);

    /// <inheritdoc />
    public static byte Id => 15;

    /// <inheritdoc />
    public int GetMaxSize()
    {
        ValidateForSerialization();

        return Mode switch
        {
            GameMode.CaptureTheFlag => FixedSize + CaptureTheFlagStateSize,
            GameMode.TerritoryControl => checked(
                FixedSize + sizeof(byte) + (TerritorySize * Territories.Length)),
            _ => throw new InvalidOperationException($"Unsupported game mode: {(byte)Mode}.")
        };
    }

    /// <inheritdoc />
    public int Serialize(Span<byte> destination)
    {
        ValidateForSerialization();

        var writer = new SpanWriter(destination);
        writer.WriteByte(Id);
        writer.WriteByte(PlayerId);
        writer.WriteColor(FogColor);
        writer.WriteColor(Team1Color);
        writer.WriteColor(Team2Color);
        WriteFixedTeamName(ref writer, Team1Name);
        WriteFixedTeamName(ref writer, Team2Name);
        writer.WriteByte((byte)Mode);

        switch (Mode)
        {
            case GameMode.CaptureTheFlag:
                WriteCaptureTheFlagState(ref writer, CaptureTheFlag!.Value);
                break;

            case GameMode.TerritoryControl:
                writer.WriteByte((byte)Territories.Length);
                foreach (TerritoryState territory in Territories.Span)
                {
                    writer.WriteVector3(territory.Position);
                    writer.WriteByte(territory.Team);
                }
                break;

            default:
                throw new InvalidOperationException($"Unsupported game mode: {(byte)Mode}.");
        }

        return writer.WrittenCount;
    }

    /// <inheritdoc />
    public static StateDataPacket Deserialize(ReadOnlySpan<byte> source)
    {
        PacketValidation.ThrowIfTooShort(source, FixedSize);

        var reader = new SpanReader(source);
        PacketValidation.ThrowIfInvalidPacketId(reader.ReadByte(), Id);

        var playerId = reader.ReadByte();
        var fogColor = reader.ReadColor();
        var team1Color = reader.ReadColor();
        var team2Color = reader.ReadColor();
        var team1Name = ReadFixedTeamName(ref reader);
        var team2Name = ReadFixedTeamName(ref reader);
        var modeId = reader.ReadByte();
        var mode = (GameMode)modeId;

        CaptureTheFlagState? captureTheFlag = null;
        ReadOnlyMemory<TerritoryState> territories = ReadOnlyMemory<TerritoryState>.Empty;

        switch (mode)
        {
            case GameMode.CaptureTheFlag:
                captureTheFlag = ReadCaptureTheFlagState(ref reader);
                break;

            case GameMode.TerritoryControl:
                territories = ReadTerritories(ref reader);
                break;

            default:
                throw new InvalidDataException($"Unsupported game mode: {modeId}.");
        }

        return new StateDataPacket(
            playerId,
            fogColor,
            team1Color,
            team2Color,
            team1Name,
            team2Name,
            mode,
            captureTheFlag,
            territories
        );
    }


    private void ValidateForSerialization()
    {
        ValidateTeamName(Team1Name, nameof(Team1Name));
        ValidateTeamName(Team2Name, nameof(Team2Name));

        switch (Mode)
        {
            case GameMode.CaptureTheFlag:
                if (CaptureTheFlag is not CaptureTheFlagState ctfState)
                {
                    throw new InvalidOperationException(
                        "Capture the Flag mode requires CaptureTheFlag state data.");
                }

                bool team1IntelIsHeld = (ctfState.IntelFlags & 0b0000_0010) != 0;
                bool team2IntelIsHeld = (ctfState.IntelFlags & 0b0000_0001) != 0;
                if (ctfState.Team1Intel.IsHeld != team1IntelIsHeld ||
                    ctfState.Team2Intel.IsHeld != team2IntelIsHeld)
                {
                    throw new InvalidOperationException(
                        "Intel held states must agree with the CTF intel flags.");
                }
                break;

            case GameMode.TerritoryControl:
                if (Territories.Length > MaximumTerritories)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(Territories),
                        $"Territory Control supports at most {MaximumTerritories} territories.");
                }
                break;

            default:
                throw new InvalidOperationException($"Unsupported game mode: {(byte)Mode}.");
        }
    }

    private static void ValidateTeamName(string name, string parameterName)
    {
        var encodedLength = Cp437.GetByteCount(name);
        if (encodedLength > TeamNameSize)
        {
            throw new ArgumentException(
                $"A team name cannot exceed {TeamNameSize} CP437 bytes.",
                parameterName);
        }
    }

    private static void WriteFixedTeamName(ref SpanWriter writer, string name)
    {
        var nameBytes = Cp437.GetBytes(name);
        writer.WriteBytes(nameBytes);

        Span<byte> padding = stackalloc byte[TeamNameSize];
        padding.Clear();
        writer.WriteBytes(padding[..(TeamNameSize - nameBytes.Length)]);
    }

    private static string ReadFixedTeamName(ref SpanReader reader)
    {
        var nameBytes = reader.ReadBytes(TeamNameSize);
        int contentLength = nameBytes.Length;
        while (contentLength > 0 && nameBytes[contentLength - 1] == 0)
        {
            contentLength--;
        }

        var content = nameBytes[..contentLength];
        return Cp437.GetString(content);
    }

    private static CaptureTheFlagState ReadCaptureTheFlagState(ref SpanReader reader)
    {
        PacketValidation.ThrowIfInvalidLength(reader.RemainingSpan, CaptureTheFlagStateSize);

        var team1Score = reader.ReadByte();
        var team2Score = reader.ReadByte();
        var captureLimit = reader.ReadByte();
        var intelFlags = reader.ReadByte();
        var team1IntelIsHeld = (intelFlags & 0b0000_0010) != 0;
        var team2IntelIsHeld = (intelFlags & 0b0000_0001) != 0;
        IntelLocationData team1Intel = ReadIntelLocation(ref reader, team1IntelIsHeld);
        IntelLocationData team2Intel = ReadIntelLocation(ref reader, team2IntelIsHeld);

        var team1Base = reader.ReadVector3();
        var team2Base = reader.ReadVector3();

        return new CaptureTheFlagState(
            team1Score,
            team2Score,
            captureLimit,
            intelFlags,
            team1Intel,
            team2Intel,
            team1Base,
            team2Base);
    }

    private static IntelLocationData ReadIntelLocation(ref SpanReader reader, bool isHeld)
    {
        if (isHeld)
        {
            byte holdingPlayerId = reader.ReadByte();
            _ = reader.ReadBytes(11);
            return new IntelLocationData(true, holdingPlayerId, Vector3.Zero);
        }

        var groundPosition = reader.ReadVector3();
        return new IntelLocationData(false, 0, groundPosition);
    }

    private static ReadOnlyMemory<TerritoryState> ReadTerritories(ref SpanReader reader)
    {
        PacketValidation.ThrowIfTooShort(reader.RemainingSpan, sizeof(byte));

        var territoryCount = reader.ReadByte();
        if (territoryCount > MaximumTerritories)
        {
            throw new InvalidDataException(
                $"Territory Control packets cannot contain more than {MaximumTerritories} territories.");
        }

        var expectedTerritoryBytes = territoryCount * TerritorySize;
        var remainingBytes = reader.Remaining;
        var hasFixedArrayPadding = remainingBytes == MaximumTerritories * TerritorySize;
        if (remainingBytes != expectedTerritoryBytes && !hasFixedArrayPadding)
        {
            throw new InvalidDataException(
                "Territory Control data does not contain the declared number of territories.");
        }

        var territories = new TerritoryState[territoryCount];
        for (int i = 0; i < territories.Length; i++)
        {
            var position = reader.ReadVector3();
            var team = reader.ReadByte();
            territories[i] = new TerritoryState(position, team);
        }

        if (reader.Remaining > 0)
        {
            ReadOnlySpan<byte> padding = reader.ReadBytes(reader.Remaining);
            foreach (var value in padding)
            {
                if (value != 0)
                {
                    throw new InvalidDataException(
                        "Territory Control array padding must contain only zero bytes.");
                }
            }
        }

        return territories;
    }

    private static void WriteCaptureTheFlagState(ref SpanWriter writer, CaptureTheFlagState state)
    {
        writer.WriteByte(state.Team1Score);
        writer.WriteByte(state.Team2Score);
        writer.WriteByte(state.CaptureLimit);
        writer.WriteByte(state.IntelFlags);

        bool team1IntelIsHeld = (state.IntelFlags & 0b0000_0010) != 0;
        bool team2IntelIsHeld = (state.IntelFlags & 0b0000_0001) != 0;
        WriteIntelLocation(ref writer, state.Team1Intel, team1IntelIsHeld);
        WriteIntelLocation(ref writer, state.Team2Intel, team2IntelIsHeld);

        writer.WriteVector3(state.Team1Base);
        writer.WriteVector3(state.Team2Base);
    }

    private static void WriteIntelLocation(
        ref SpanWriter writer,
        IntelLocationData intelLocation,
        bool isHeld)
    {
        if (isHeld)
        {
            writer.WriteByte(intelLocation.HoldingPlayerId);
            Span<byte> padding = stackalloc byte[11];
            padding.Clear();
            writer.WriteBytes(padding);
            return;
        }

        writer.WriteVector3(intelLocation.GroundPosition);
    }
}

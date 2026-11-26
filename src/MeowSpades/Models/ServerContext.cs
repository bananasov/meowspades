using Arch.Core;
using Enet;

namespace MeowSpades.Models;

public sealed class ServerContext
{
    private readonly PlayerSlot[] _slots;
    private readonly Dictionary<uint, byte> _connectIdToSlot;
    public float DeltaTime { get; set; }

    public ServerContext(int maxPlayers = 32)
    {
        if (maxPlayers is < 1 or > 255)
            throw new ArgumentOutOfRangeException(nameof(maxPlayers));

        _slots = new PlayerSlot[maxPlayers];
        _connectIdToSlot = [with(maxPlayers)];
    }

    public int FindNextSlot() => Array.FindIndex(_slots, s => !s.IsOccupied);

    public IEnumerable<PlayerSlot> GetPlayers()
    {
        foreach (var slot in _slots)
        {
            if (slot.IsOccupied)
            {
                yield return slot;
            }
        }
    }

    public bool TryAllocate(EnetPeer peer, Entity entity, byte slot)
    {
        // Manually pass in the slot so we can use the slot in other places as well
        if (slot < 0)
        {
            return false;
        }

        _slots[slot] = new PlayerSlot { IsOccupied = true, Entity = entity, Peer = peer };
        _connectIdToSlot[peer.ConnectId] = slot;
        return true;
    }

    public bool TryRelease(EnetPeer peer, out byte id, out Entity entity)
    {
        if (_connectIdToSlot.Remove(peer.ConnectId, out id))
        {
            entity = _slots[id].Entity;
            _slots[id] = default;
            return true;
        }

        entity = default;
        return false;
    }

    public bool TryGet(byte id, out PlayerSlot slot)
    {
        if (id < _slots.Length && _slots[id].IsOccupied)
        {
            slot = _slots[id];
            return true;
        }

        slot = default;
        return false;
    }

    public bool TryGetByPeer(EnetPeer peer, out byte id, out PlayerSlot slot)
    {
        if (_connectIdToSlot.TryGetValue(peer.ConnectId, out id))
        {
            return TryGet(id, out slot);
        }

        slot = default;
        return false;
    }
}

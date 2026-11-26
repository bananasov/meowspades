using System.Collections.Concurrent;
using Arch.Core;
using Enet;

namespace MeowSpades.Models;

public sealed class ServerContext
{
    private readonly PlayerSlot[] _slots;
    private readonly Dictionary<uint, byte> _connectIdToSlot;
    private readonly ConcurrentQueue<OutboundPacket> _outboundPackets = new();
    public float DeltaTime { get; set; }

    public ServerContext(int maxPlayers = 32)
    {
        if (maxPlayers is < 1 or > 255)
            throw new ArgumentOutOfRangeException(nameof(maxPlayers));

        _slots = new PlayerSlot[maxPlayers];
        _connectIdToSlot = [with(maxPlayers)];
    }

    public int FindNextSlot() => Array.FindIndex(_slots, s => !s.IsOccupied);

    public bool TryAllocate(EnetPeer peer, Entity entity, byte slot)
    {
        // Manually pass in the slot so we can use the slot in other places as well
        if (slot >= _slots.Length || _slots[slot].IsOccupied || _connectIdToSlot.ContainsKey(peer.ConnectId))
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

    /// <summary>Queues a packet for delivery to a connected peer.</summary>
    /// <param name="connectId">The unique ENet connection identifier of the recipient.</param>
    /// <param name="payload">The packet payload; ownership transfers to the queue and the array must not be modified afterward.</param>
    /// <param name="channelId">The ENet channel on which to send the packet.</param>
    /// <param name="reliable">A value indicating whether ENet should deliver the packet reliably.</param>
    public void EnqueueOutbound(uint connectId, byte[] payload, byte channelId = 0, bool reliable = true)
    {
        ArgumentNullException.ThrowIfNull(payload);
        _outboundPackets.Enqueue(new OutboundPacket(connectId, channelId, reliable, payload));
    }

    /// <summary>Removes the next queued packet for egress.</summary>
    /// <param name="packet">When this method returns, contains the next queued packet if one was available.</param>
    /// <returns><see langword="true" /> if a packet was dequeued; otherwise, <see langword="false" />.</returns>
    internal bool TryDequeueOutbound(out OutboundPacket packet)
    {
        if (_outboundPackets.TryDequeue(out var queuedPacket))
        {
            packet = queuedPacket;
            return true;
        }

        packet = default;
        return false;
    }

    /// <summary>Looks up the player slot associated with an ENet connection.</summary>
    /// <param name="connectId">The unique ENet connection identifier.</param>
    /// <param name="id">When this method returns, contains the player's slot identifier.</param>
    /// <param name="slot">When this method returns, contains the occupied player slot.</param>
    /// <returns><see langword="true" /> if the connection is still allocated; otherwise, <see langword="false" />.</returns>
    public bool TryGetByConnectId(uint connectId, out byte id, out PlayerSlot slot)
    {
        if (_connectIdToSlot.TryGetValue(connectId, out id) && TryGet(id, out slot))
        {
            return true;
        }

        id = default;
        slot = default;
        return false;
    }

    public bool TryGetByPeer(EnetPeer peer, out byte id, out PlayerSlot slot) =>
        TryGetByConnectId(peer.ConnectId, out id, out slot);

    internal readonly record struct OutboundPacket(
        uint ConnectId,
        byte ChannelId,
        bool Reliable,
        byte[] Payload
    );
}

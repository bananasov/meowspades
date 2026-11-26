using Arch.Core;
using Arch.System;
using Enet;
using MeowSpades.Models;
using Serilog;

namespace MeowSpades.Systems;

/// <summary>Sends queued outbound packets to their connected peers after world updates.</summary>
public sealed class EgressSystem(World world, ManagedEnetHost host) : BaseSystem<World, ServerContext>(world)
{
    /// <inheritdoc />
    public override void AfterUpdate(in ServerContext context)
    {
        var queuedAnyPacket = false;

        while (context.TryDequeueOutbound(out var outbound))
        {
            if (!context.TryGetByConnectId(outbound.ConnectId, out _, out var slot))
            {
                continue;
            }

            if (outbound.ChannelId >= slot.Peer.ChannelCount)
            {
                Log.Warning("Dropped outbound packet for connection {ConnectId}: channel {ChannelId} is unavailable", outbound.ConnectId, outbound.ChannelId);
                continue;
            }

            var flags = outbound.Reliable ? EnetPacketFlag.Reliable : 0;
            var packet = EnetPacket.Create(outbound.Payload, flags);
            if (!packet.IsCreated)
            {
                Log.Error("Failed to allocate outbound ENet packet for connection {ConnectId}", outbound.ConnectId);
                continue;
            }

            try
            {
                if (slot.Peer.TrySend(outbound.ChannelId, ref packet))
                {
                    queuedAnyPacket = true;
                }
                else
                {
                    Log.Warning("Failed to queue outbound packet for connection {ConnectId}", outbound.ConnectId);
                }
            }
            finally
            {
                packet.Dispose();
            }
        }

        if (queuedAnyPacket)
        {
            host.Flush();
        }
    }
}

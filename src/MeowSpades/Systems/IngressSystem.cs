using Arch.Core;
using Arch.System;
using Enet;
using MeowSpades.Components;
using MeowSpades.Models;
using MeowSpades.Network;
using Serilog;

namespace MeowSpades.Systems;

public sealed class IngressSystem(World world, ManagedEnetHost host) : BaseSystem<World, ServerContext>(world)
{
    public override void BeforeUpdate(in ServerContext context)
    {
        if (host.Service(5, out var @event) > 0)
        {
            switch (@event.Type)
            {
                case EnetEventType.Connect:
                    OnClientConnect(@event, context);
                    break;

                case EnetEventType.Disconnect:
                    OnClientDisconnect(@event, context);
                    break;

                case EnetEventType.Receive:
                    break;
            }
        }
    }

    private void OnClientConnect(EnetEvent @event, ServerContext context)
    {
        var protocolVersion = (ProtocolVersion)@event.Data;
        if (protocolVersion != ProtocolVersion.Version075)
        {
            Log.Information("Disconnected client for wrong packet version");
            @event.Peer.DisconnectNow((uint)DisconnectReason.WrongProtocolVersion);
            return;
        }

        var id = context.FindNextSlot();
        var entity = World.Create(new Player
        {
            EnetPeerId = @event.Peer.IncomingPeerId,
            PlayerID = (byte)id,
            State = PlayerState.Disconnected,
        });
        context.TryAllocate(@event.Peer, entity, (byte)id);

        Log.Information("New client created with {id}", id);
    }

    private void OnClientDisconnect(EnetEvent @event, ServerContext context)
    {
        var peer = @event.Peer;
        context.TryRelease(peer, out var id, out var _);

        Log.Information("Client {address} disconnected with {id}", peer.Address, id);
    }
}

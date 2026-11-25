using Arch.Core;
using Arch.System;
using Enet;
using MeowSpades.Network;
using Serilog;

namespace MeowSpades.Systems;

public sealed class IngressSystem(World world, ManagedEnetHost host) : BaseSystem<World, float>(world)
{
    public override void BeforeUpdate(in float deltaTime)
    {
        if (host.Service(5, out var @event) > 0)
        {
            switch (@event.Type)
            {
                case EnetEventType.Connect:
                    OnClientConnect(@event);
                    break;

                case EnetEventType.Disconnect:
                    OnClientConnect(@event);
                    break;

                case EnetEventType.Receive:
                    break;
            }
        }
    }

    private void OnClientConnect(EnetEvent @event)
    {
        var protocolVersion = (ProtocolVersion)@event.Data;
        if (protocolVersion != ProtocolVersion.Version075)
        {
            Log.Information("Disconnected client for wrong packet version");
            @event.Peer.DisconnectNow(3);
            return;
        }

        // TODO: Player slot checking

        // var entity = World.Create(new AosPeerComponent
        // {
        //     EnetPeerId = netEvent.Peer.ID,
        //     PlayerId = playerId,
        //     State = ClientConnectionState.AwaitingExistingPlayer
        // });

    }

    private void OnClientDisconnect(EnetEvent @event)
    {
        var peer = @event.Peer;

        Log.Information($"disconnected - IncomingPeerId: {peer.IncomingPeerId}, Address: {peer.Address}");
    }
}

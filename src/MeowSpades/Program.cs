using System.Net;
using Arch.Core;
using enet;
using Enet;

const ushort DEFAULT_SERVER_PORT = 32887;
const nuint MAX_CLIENTS = 16;

ENetAddress.FromIpAddress(IPAddress.IPv6Any, DEFAULT_SERVER_PORT, out var address);

using var host = ManagedEnetHost.Create(address, MAX_CLIENTS, 0, 0, 0, EnetHostOption.Ipv6DualMode);
host.TrySetCompressorWithRangeCoder();

using var world = World.Create();
Console.WriteLine("Hello, World!");

while (!Console.KeyAvailable)
{
    if (host.Service(5, out var @event) > 0)
    {
        var peer = @event.Peer;
        switch (@event.Type)
        {
            case EnetEventType.Connect:
                Console.WriteLine($"[Server] connected - IncomingPeerId: {peer.IncomingPeerId}, Address: {peer.Address}");
                break;

            case EnetEventType.Disconnect:
                Console.WriteLine($"[Server] disconnected - IncomingPeerId: {peer.IncomingPeerId}, Address: {peer.Address}");
                break;

            case EnetEventType.Receive:
                break;
        }

        if (host.CheckEvents(out _) <= 0)
            break;
    }
}

using Arch.Core;
using Enet;

namespace MeowSpades.Models;

public struct PlayerSlot
{
    public bool IsOccupied;
    public Entity Entity;
    public EnetPeer Peer;
}

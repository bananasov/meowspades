using Arch.Core;
using Arch.System;
using Enet;
using MeowSpades.Components;
using MeowSpades.Models;
using MeowSpades.Network;
using Serilog;

namespace MeowSpades.Systems;

public sealed class WorldUpdateSystem(World world) : BaseSystem<World, ServerContext>(world)
{
    public override void Update(in ServerContext context)
    {
        foreach (var playerSlot in context.GetPlayers())
        {
            var player = World.Get<Player>(playerSlot.Entity);

            switch (player.State)
            {
                case PlayerState.Disconnected:
                    break;

                case PlayerState.StartingMap:
                    // TODO: Handle map startup.
                    break;

                case PlayerState.LoadingChunks:
                    // TODO: Handle chunk loading.
                    break;

                case PlayerState.Joining:
                    // TODO: Handle players joining.
                    break;

                case PlayerState.Spawning:
                    // TODO: Handle player spawning.
                    break;

                case PlayerState.WaitingForRespawn:
                    // TODO: Handle respawning.
                    break;

                case PlayerState.Ready:
                    // TODO: Handle ready players.
                    break;
            }
        }
    }
}

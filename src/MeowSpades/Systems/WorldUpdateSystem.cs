using Arch.Core;
using Arch.System;
using MeowSpades.Components;
using MeowSpades.Models;
using MeowSpades.Network;

namespace MeowSpades.Systems;

public sealed class WorldUpdateSystem(World world) : BaseSystem<World, ServerContext>(world)
{
    private readonly QueryDescription playerQuery = new QueryDescription().WithAll<Player>();

    public override void Update(in ServerContext context)
    {
        world.Query(in playerQuery, (Entity entity, ref Player player) =>
        {
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
        });
    }
}

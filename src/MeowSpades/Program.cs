using System.Diagnostics;
using System.Net;
using Arch.Core;
using Arch.System;
using enet;
using Enet;
using MeowSpades.Models;
using MeowSpades.Systems;
using Serilog;

const ushort DEFAULT_SERVER_PORT = 32887;
const nuint MAX_CLIENTS = 16;

using var log = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
Log.Logger = log;

ENetAddress.FromIpAddress(IPAddress.IPv6Any, DEFAULT_SERVER_PORT, out var address);

Log.Information("Creating Enet host");
using var host = ManagedEnetHost.Create(address, MAX_CLIENTS, 0, 0, 0, EnetHostOption.Ipv6DualMode);
host.TrySetCompressorWithRangeCoder();

Log.Information("Creating World and initializing systems");
using var world = World.Create();
var context = new ServerContext(32);
var _systems = new Group<ServerContext>(
    "Systems",
    new IngressSystem(world, host)
);
_systems.Initialize();

var stopwatch = Stopwatch.StartNew();
long previousTicks = stopwatch.ElapsedTicks;
double tickFrequency = 1.0 / Stopwatch.Frequency;

Log.Information("Starting main loop");

// TODO: Proper Ctrl+C handling
while (!Console.KeyAvailable)
{
    long currentTicks = stopwatch.ElapsedTicks;
    var deltaTime = (float)((currentTicks - previousTicks) * tickFrequency);
    previousTicks = currentTicks;
    context.DeltaTime = deltaTime;

    _systems.BeforeUpdate(in context);
    _systems.Update(in context);
    _systems.AfterUpdate(in context);
}

_systems.Dispose();

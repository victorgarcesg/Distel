using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans.Configuration;

using var host = Host.CreateDefaultBuilder(args)
    .UseOrleans(siloBuilder =>
    {
        siloBuilder.Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "dev";
            options.ServiceId = "DistelService";
        })
        .UseLocalhostClustering(siloPort: 11111, gatewayPort: 30000)
        .ConfigureLogging(logging => logging.AddConsole());
    })
    .Build();

await host.RunAsync();
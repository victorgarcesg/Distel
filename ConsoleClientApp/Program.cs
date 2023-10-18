using Distel.Grains.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans.Configuration;

using var host = Host.CreateDefaultBuilder(args)
    .UseOrleansClient(clientBuilder =>
        clientBuilder
            .UseLocalhostClustering()
            .Configure<ClusterOptions>(options =>
            {
                options.ClusterId = "dev";
                options.ServiceId = "DistelService";
            })
        )
    .ConfigureLogging(logging => logging.AddConsole())
    .Build();

await host.StartAsync();

var client = host.Services.GetRequiredService<IClusterClient>();

while (true)
{
    Console.Write("Please enter a guest name. Type 'exit' to close: ");
    var guest = Console.ReadLine();
    if (guest == "exit")
        break;
    await SendWelcomeGreeting(client, guest!);
    await CalculateDiscount(client);
    await MoveGuest(client);
}

static async Task MoveGuest(IClusterClient client)
{
    var tajMahal = client.GetGrain<IHotelGrain>("TajMahal");
    var charminar = client.GetGrain<IHotelGrain>("Charminar");
    await Task.WhenAll(
        tajMahal.OnBoardFromOtherHotel(charminar, "Shyam"),
        charminar.OnBoardFromOtherHotel(tajMahal, "Ram")
    );
}

await host.StopAsync();

static async Task SendWelcomeGreeting(IClusterClient client, string guest)
{
    var hotel = client.GetGrain<IHotelGrain>("Distel.Agra");
    Console.WriteLine("Hotel Grain PrimaryKey : " + await hotel.GetKey());

    var response = await hotel.WelcomeGreetingAsync(guest);
    Console.WriteLine($"\n\n{response}\n");
}

static async Task CalculateDiscount(IClusterClient client)
{
    var discountWorker = client.GetGrain<IDiscountCalculator>(0);
    var discount = await discountWorker.ComputeDiscount(150);
    Console.WriteLine($"Discount for the Amount ${150} is ${discount}");
}
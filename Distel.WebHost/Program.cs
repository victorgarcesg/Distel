using Orleans.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Host.UseOrleans(siloBuilder =>
{
    siloBuilder
        .UseLocalhostClustering()
        .Configure<ClusterOptions>(opts =>
        {
            opts.ClusterId = "dev";
            opts.ServiceId = "DistelAPI";
        })
        .Configure<EndpointOptions>(opts =>
        {
            opts.AdvertisedIPAddress = System.Net.IPAddress.Loopback;
        })
        .ConfigureLogging(logging => logging.AddConsole())
        .AddCosmosGrainStorageAsDefault(opt =>
        {
            opt.DatabaseName = "Orleans";
            opt.ContainerName = "OrleansStorage";
            opt.ConfigureCosmosClient("AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");
            opt.IsResourceCreationEnabled = true;
        });
});

var app = builder
    .Build();

app.UseRouting();

#pragma warning disable ASP0014
app.UseEndpoints(e => e.MapControllers());
#pragma warning restore ASP0014

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.Run();

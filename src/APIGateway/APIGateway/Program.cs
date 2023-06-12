using APIGateway;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddOcelot( builder.Configuration )
    .AddSingletonDefinedAggregator<CatalogItemAggregator>();

builder.Services.AddAuthentication()
    .AddJwtBearer("Bearer", o =>
    {
        o.Authority = "http://localhost:8081/realms/epam-training";
        o.RequireHttpsMetadata = false;
    });

builder.Services.AddHttpClient();

var app = builder.Build();

app.UseOcelot().Wait();

app.Run();

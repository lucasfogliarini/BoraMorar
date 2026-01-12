var builder = DistributedApplication.CreateBuilder(args);

builder.AddBoraKeycloakSystem()
       .AddBoraMorarSystem()
       .AddToAspire();

var app = builder.Build();

await app.RunAsync();
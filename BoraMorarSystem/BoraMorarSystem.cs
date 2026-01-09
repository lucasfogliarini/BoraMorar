var builder = DistributedApplication.CreateBuilder(args);

builder.AddKeycloakBoraMorarSystem()
        .AddBoraMorarSystem()
        .AddToAspire();

var app = builder.Build();

await app.RunAsync();
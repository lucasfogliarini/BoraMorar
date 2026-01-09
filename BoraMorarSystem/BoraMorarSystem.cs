var builder = DistributedApplication.CreateBuilder(args);

var system = builder.AddKeycloakBoraMorarSystem();
builder.AddToAspire(system);

var app = builder.Build();

await app.RunAsync();
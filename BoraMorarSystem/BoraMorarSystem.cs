var builder = DistributedApplication.CreateBuilder(args);

var keycloakSystem = builder.AddKeycloakBoraMorarSystem();
var boraMorarSystem = builder.AddBoraMorarSystem();
builder.AddToAspire(boraMorarSystem);

var app = builder.Build();

await app.RunAsync();
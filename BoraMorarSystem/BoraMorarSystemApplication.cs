var boraMorarSystem = SoftwareSystem.CreateBuilder<BoraMorarSystem>();

var app = boraMorarSystem.Builder.Build();

await app.RunAsync();
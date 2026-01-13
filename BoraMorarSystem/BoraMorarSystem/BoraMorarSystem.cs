using Aspire.Hosting;
using Aspire.Hosting.JavaScript;

public class BoraMorarSystem(IDistributedApplicationBuilder builder) : BoraKeycloakSystem(builder)
{
    const string SystemName = nameof(BoraMorarSystem);
    protected override string Name { get; init; } = SystemName;
    protected override string SystemDiagramUrl { get; init; } = $"https://bora.earth/work/{SystemName}/";
    public Service<ProjectResource> BoraWebApi { get { return GetService<Service<ProjectResource>>(); } }
    public Service<JavaScriptAppResource> BoraWebApp { get { return GetService<Service<JavaScriptAppResource>>(); } }

    public override IResourceBuilder<ExternalServiceResource> AddSystem()
    {
        var system = base.AddSystem();
        AddBoraWebApi();
        AddBoraWebApp();
        return system
                .WithChildRelationship(BoraWebApi.Resource)
                .WithChildRelationship(BoraWebApp.Resource);
    }

    private void AddBoraWebApi()
    {
        var boraWebApiService = AddService<Service<ProjectResource>>(nameof(BoraWebApi));
        boraWebApiService.Resource = Builder.AddProject(BoraWebApi.Name, "../BoraMorar.WebApi")
                .WithReferenceRelationship(KeycloakService.Resource)
                .WaitFor(KeycloakService.Resource)
                .WithHttpEndpoint(name: BoraWebApi.Name, port: BoraWebApi.Port, isProxied: false);
    }

    private void AddBoraWebApp()
    {
        var boraWebAppService = AddService<Service<JavaScriptAppResource>>(nameof(BoraWebApp));
        boraWebAppService.Resource = Builder.AddJavaScriptApp(boraWebAppService.Name, "../BoraMorar.WebApp")
                            .WaitFor(BoraWebApi.Resource)
                            .WithHttpEndpoint(name: boraWebAppService.Name, port: boraWebAppService.Port, isProxied: false)
                            .WithEnvironment("PORT", boraWebAppService.Port.ToString());
    }
}
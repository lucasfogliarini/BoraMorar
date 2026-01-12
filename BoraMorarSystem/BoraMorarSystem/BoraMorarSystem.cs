public class BoraMorarSystem(BoraKeycloakSystem boraKeycloakSystem) : SystemC4(boraKeycloakSystem.Builder, 3000)
{
    const string SystemName = nameof(BoraMorarSystem);
    protected override string Name { get; init; } = SystemName;
    protected override string Url { get; init; } = $"https://bora.earth/work/{SystemName}/";

    public override IResourceBuilder<ExternalServiceResource> AddToResources()
    {
        var webApiResource = AddBoraWebApi();
        var webAppResource = AddBoraWebApp(webApiResource);
        return base.AddToResources()
                   .WithChildRelationship(webApiResource)
                   .WithChildRelationship(webAppResource);
    }

    private IResourceBuilder<ProjectResource> AddBoraWebApi()
    {
        IResourceBuilder<ProjectResource> projectResource = Builder.AddProject(MainService.Name, "../BoraMorar.WebApi")
                .WithReferenceRelationship(boraKeycloakSystem.KeycloakResource)
                .WaitFor(boraKeycloakSystem.KeycloakResource)
                .WithHttpEndpoint(name: MainService.Name, port: MainService.Port, isProxied: false);

        return projectResource;
    }

    private IResourceBuilder<IResource> AddBoraWebApp(IResourceBuilder<ProjectResource> boraWebApiResource)
    {
        var boraMorarWebApp = "boramorar-app";
        var webApp = Builder.AddJavaScriptApp(boraMorarWebApp, "../BoraMorar.WebApp", "dev")
                            .WaitFor(boraWebApiResource)
                            .WithHttpEndpoint(name: boraMorarWebApp, port: MainService.Port + 1, isProxied: false);
        return webApp;
    }
}
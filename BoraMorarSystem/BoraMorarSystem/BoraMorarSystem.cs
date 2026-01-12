public class BoraMorarSystem(BoraKeycloakSystem boraKeycloakSystem) : SystemC4(boraKeycloakSystem.Builder, 3000)
{
    const string SystemName = nameof(BoraMorarSystem);
    protected override string Name { get; init; } = SystemName;
    protected override string Url { get; init; } = $"https://bora.earth/work/{SystemName}/";

    public override IResourceBuilder<ExternalServiceResource> AddToResources()
    {
        var projectResource = AddBoraApi();
        return base.AddToResources()
                   .WithChildRelationship(projectResource);
    }

    private IResourceBuilder<ProjectResource> AddBoraApi()
    {
        IResourceBuilder<ProjectResource> projectResource = Builder.AddProject(MainService.Name, "../BoraMorar.WebApi")
                .WithReferenceRelationship(boraKeycloakSystem.KeycloakResource)
                .WaitFor(boraKeycloakSystem.KeycloakResource)
                .WithHttpEndpoint(name: MainService.Name, port: MainService.Port, isProxied: false);

        return projectResource;
    }
}
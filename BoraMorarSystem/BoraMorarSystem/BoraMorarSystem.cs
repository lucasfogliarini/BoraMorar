public class BoraMorarSystem(BoraKeycloakSystem boraKeycloakSystem) : SystemC4(boraKeycloakSystem.Builder)
{
    const string SystemName = nameof(BoraMorarSystem);
    protected override string Name { get; init; } = SystemName;
    protected override string Url { get; init; } = $"https://bora.earth/work/{SystemName}/";

    public override IResourceBuilder<ExternalServiceResource> AddToResources()
    {
        AddBoraApi();
        return base.AddToResources();
    }

    private void AddBoraApi()
    {
        var boramorarApi = "boramorar-api";
        Builder.AddProject(boramorarApi, "../BoraMorar.WebApi")
                .WithReferenceRelationship(boraKeycloakSystem.KeycloakResource)
                .WaitFor(boraKeycloakSystem.KeycloakResource);
    }
}
using Aspire.Hosting;

public class BoraMorarSystem(KeycloakBoraMorarSystem keycloakBoraMorarSystem) : SystemC4(SystemName, keycloakBoraMorarSystem.Builder)
{
    const string SystemName = nameof(BoraMorarSystem);
    protected override string Url { get; set; } = "https://bora.earth/work/BoraMorar/";

    public override IResourceBuilder<ExternalServiceResource> AddToResources()
    {
        AddBoraApi();
        return base.AddToResources();
    }

    private void AddBoraApi()
    {
        var boramorarApi = "boramorar-api";
        Builder.AddProject(boramorarApi, "../BoraMorar.WebApi")
                .WithReferenceRelationship(keycloakBoraMorarSystem.KeycloakResource)
                .WaitFor(keycloakBoraMorarSystem.KeycloakResource);
    }
}
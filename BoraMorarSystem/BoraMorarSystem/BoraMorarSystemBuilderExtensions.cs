public static class BoraMorarSystemBuilderExtensions
{
    public static BoraMorarSystem AddBoraMorarSystem(this KeycloakBoraMorarSystem keycloakBoraMorarSystem)
    {
        var boraMorarSystem = new BoraMorarSystem(keycloakBoraMorarSystem);
        boraMorarSystem.AddToResources();
        return boraMorarSystem;
    }
}
public static class KeycloakBoraMorarSystemBuilderExtensions
{
    public static KeycloakBoraMorarSystem AddKeycloakBoraMorarSystem(this IDistributedApplicationBuilder builder)
    {
        var keycloakBoraMorarSystem = new KeycloakBoraMorarSystem();
        keycloakBoraMorarSystem.Add(builder);
        return keycloakBoraMorarSystem;
    }
}
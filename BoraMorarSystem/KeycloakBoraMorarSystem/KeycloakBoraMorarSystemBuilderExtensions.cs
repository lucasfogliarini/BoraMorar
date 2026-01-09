public static class KeycloakBoraMorarSystemBuilderExtensions
{
    public static KeycloakBoraMorarSystem AddKeycloakBoraMorarSystem(this IDistributedApplicationBuilder builder)
    {
        var keycloakBoraMorarSystem = new KeycloakBoraMorarSystem(builder);
        keycloakBoraMorarSystem.AddToResources();
        return keycloakBoraMorarSystem;
    }
}
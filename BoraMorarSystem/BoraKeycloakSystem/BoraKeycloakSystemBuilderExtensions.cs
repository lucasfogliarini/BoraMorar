public static class BoraKeycloakSystemBuilderExtensions
{
    public static BoraKeycloakSystem AddBoraKeycloakSystem(this IDistributedApplicationBuilder builder)
    {
        var keycloakSystem = new BoraKeycloakSystem(builder);
        keycloakSystem.AddToResources();
        return keycloakSystem;
    }
}
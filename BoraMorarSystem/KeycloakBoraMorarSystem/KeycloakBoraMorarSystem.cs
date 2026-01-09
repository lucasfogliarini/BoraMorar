public class KeycloakBoraMorarSystem() : SystemC4(SystemName)
{
    const string SystemName = nameof(KeycloakBoraMorarSystem);
    public override string Url { get; set; } = "https://bora.earth/work/IdentitySystem/";
    const string PostgresPassword = $"{SystemName}!";

    IResourceBuilder<ParameterResource>? PostgresPasswordResource;
    const string BoraRealm = "bora";
    public void Add(IDistributedApplicationBuilder builder)
    {
        PostgresPasswordResource = builder.AddParameter("postgres-password", PostgresPassword, secret: false);

        var keycloakPostgresServer = AddKeycloakPostgresServer(builder);
        var keycloak = AddKeycloakServer(builder, keycloakPostgresServer);

        var boraAdmin = builder.AddExternalService($"{BoraRealm}Admin", $"{MainService.AbsolutePath}/admin/{BoraRealm}/console/");
        var boraAccount = builder.AddExternalService($"{BoraRealm}Account", $"{MainService.AbsolutePath}/realms/{BoraRealm}/account/");

        AddSystem(builder)
            .WithChildRelationship(keycloakPostgresServer)
            .WithChildRelationship(keycloak)
            .WithChildRelationship(boraAdmin)
            .WithChildRelationship(boraAccount);
    }

    /// <summary>
    /// https://www.keycloak.org
    /// </summary>
    public IResourceBuilder<KeycloakResource> AddKeycloakServer(IDistributedApplicationBuilder builder, IResourceBuilder<PostgresServerResource> keycloakPostgresServer)
    {
        var keycloak = builder.AddKeycloakContainer(MainService.Name, port: MainService.Port)
            .WaitFor(keycloakPostgresServer)
            //.WithLifetime(ContainerLifetime.Persistent)
            .WithDataVolume($"{MainService.Name}_data")
            .WithImport($"{SystemName}System/bora-realm")
            .WithEnvironment("KC_BOOTSTRAP_ADMIN_USERNAME", "admin")
            .WithEnvironment("KC_BOOTSTRAP_ADMIN_PASSWORD", "admin")

            .WithEnvironment("KC_DB", "postgres")
            .WithEnvironment("KC_DB_URL", $"jdbc:postgresql://{DatabaseServer.Name}:5432/{DatabaseServer.Database.Name}")
            .WithEnvironment("KC_DB_USERNAME", "postgres")
            .WithEnvironment("KC_DB_PASSWORD", PostgresPasswordResource)

            .WithEnvironment("KC_HEALTH_ENABLED", "true")
            .WithEnvironment("KC_METRICS_ENABLED", "true");

        return keycloak;
    }

    /// <summary>
    /// https://www.keycloak.org/server/db
    /// </summary>
    IResourceBuilder<PostgresServerResource> AddKeycloakPostgresServer(IDistributedApplicationBuilder builder)
    {
        var postgresServer = builder.AddPostgres(DatabaseServer.Name, port: DatabaseServer.Port)
            .WithLifetime(ContainerLifetime.Persistent)
            .WithDataVolume($"{DatabaseServer.Name}_data")
            //.WithPgAdmin()
            .WithPassword(PostgresPasswordResource!);
        postgresServer.AddDatabase(DatabaseServer.Database.Name);
        return postgresServer;
    }
}
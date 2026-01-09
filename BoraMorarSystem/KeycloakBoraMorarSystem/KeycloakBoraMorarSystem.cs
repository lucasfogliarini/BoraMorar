public class KeycloakBoraMorarSystem(IDistributedApplicationBuilder builder) : SystemC4(SystemName, builder)
{
    public const string SystemName = nameof(KeycloakBoraMorarSystem);
    const string PostgresPassword = $"{SystemName}!";
    public IResourceBuilder<KeycloakResource> KeycloakResource { get; private set; }
    protected override string Url { get; set; } = "https://bora.earth/work/IdentitySystem/";
    

    IResourceBuilder<ParameterResource>? PostgresPasswordResource;
    const string BoraRealm = "bora";
    public override IResourceBuilder<ExternalServiceResource> AddToResources()
    {
        PostgresPasswordResource = Builder.AddParameter("postgres-password", PostgresPassword, secret: false);

        var keycloakPostgresServer = AddKeycloakPostgresServer();
        AddKeycloakServer(keycloakPostgresServer);

        var boraAdmin = Builder.AddExternalService($"{BoraRealm}Admin", $"{MainService.AbsolutePath}/admin/{BoraRealm}/console/");
        var boraAccount = Builder.AddExternalService($"{BoraRealm}Account", $"{MainService.AbsolutePath}/realms/{BoraRealm}/account/");

        return base.AddToResources()
            .WithChildRelationship(keycloakPostgresServer)
            .WithChildRelationship(KeycloakResource)
            .WithChildRelationship(boraAdmin)
            .WithChildRelationship(boraAccount);
    }

    /// <summary>
    /// https://www.keycloak.org
    /// </summary>
    public void AddKeycloakServer(IResourceBuilder<PostgresServerResource> keycloakPostgresServer)
    {
        KeycloakResource = Builder.AddKeycloakContainer(MainService.Name, port: MainService.Port)
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
    }

    /// <summary>
    /// https://www.keycloak.org/server/db
    /// </summary>
    IResourceBuilder<PostgresServerResource> AddKeycloakPostgresServer()
    {
        var postgresServer = Builder.AddPostgres(DatabaseServer.Name, port: DatabaseServer.Port)
            .WithLifetime(ContainerLifetime.Persistent)
            .WithDataVolume($"{DatabaseServer.Name}_data")
            //.WithPgAdmin()
            .WithPassword(PostgresPasswordResource!);
        postgresServer.AddDatabase(DatabaseServer.Database.Name);
        return postgresServer;
    }
}
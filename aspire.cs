#:sdk Aspire.AppHost.Sdk@13.1.0
#:package Aspire.Hosting.PostgreSQL@13.1.0
#:package Keycloak.AuthServices.Aspire.Hosting@0.2.0

using Aspire.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = DistributedApplication.CreateBuilder(args);

builder.Configuration["ASPIRE_ALLOW_UNSECURED_TRANSPORT"] = "true";
builder.Configuration["ASPIRE_DASHBOARD_OTLP_HTTP_ENDPOINT_URL"] = "http://localhost:5001";
builder.Configuration["ASPNETCORE_URLS"] = "http://localhost:5000";

var postgresPasswordParam = builder.AddParameter("postgres-password", "BoraMorar!123", secret: false);
const string keycloakDatabaseName = "keycloak-database";
var postgresServer = builder.AddPostgres("postgres", port: 2000)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume("aspire_postgres_data")
    .WithPassword(postgresPasswordParam);
var keycloakDatabase = postgresServer.AddDatabase(keycloakDatabaseName);

// Add Keycloak for authentication
//https://www.keycloak.org/server/db
var keycloak = builder.AddKeycloakContainer("keycloak", port: 2001)
    .WaitFor(keycloakDatabase)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume("aspire_keycloak_data")
    //.WithImport("ledgerflow-realm-export.json")
    .WithEnvironment("KC_BOOTSTRAP_ADMIN_USERNAME", "admin")
    .WithEnvironment("KC_BOOTSTRAP_ADMIN_PASSWORD", "admin")

    .WithEnvironment("KC_DB", "postgres")
    .WithEnvironment("KC_DB_URL", $"jdbc:postgresql://postgres:5432/{keycloakDatabaseName}")
    .WithEnvironment("KC_DB_USERNAME", "postgres")
    .WithEnvironment("KC_DB_PASSWORD", postgresPasswordParam)

    // Recommended production flags
    .WithEnvironment("KC_HEALTH_ENABLED", "true")
    .WithEnvironment("KC_METRICS_ENABLED", "true");

var app = builder.Build();

// Structurize C4 Pattern (https://docs.structurizr.com/dsl)
// builder.GenerateStructurizeSystem();

// Backstage Pattern (https://backstage.io/docs/features/software-catalog/system-model/)
// builder.GenerateBackstageSystem();

await app.RunAsync();
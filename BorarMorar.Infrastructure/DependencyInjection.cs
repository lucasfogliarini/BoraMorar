using BoraMorar.Cotacoes.Repository;
using BoraMorar.Imoveis.Repository;
using BoraMorar.Infrastructure;
using BoraMorar.Infrastructure.DomainEvents;
using BoraMorar.Infrastructure.Repositories;
using BoraMorar.Propostas.Repository;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext();
        services.AddRepositories();
        services.AddOpenTelemetryExporter();
        services.AddScoped<IProducer>(provider => new KafkaProducer("kafka:9092"));
    }
    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IImovelRepository, ImovelRepository>();
        services.AddScoped<ICotacaoRepository, CotacaoRepository>();
        services.AddScoped<IPropostaRepository, PropostaRepository>();
    }
    private static void AddDbContext(this IServiceCollection services)
    {
        services.AddScoped<DomainEventsDispatcher>();
        services.AddDbContext<BoraMorarDbContext>(options => options.UseInMemoryDatabase(nameof(BoraMorarDbContext)));
    }

    private static void AddOpenTelemetryExporter(this IServiceCollection services)
    {
        var assemblyName = Assembly.GetEntryAssembly()?.GetName();
        var serviceName = assemblyName?.Name ?? "Unknown Service Name";
        var serviceVersion = assemblyName?.Version?.ToString() ?? "Unknown Version";

        services.AddOpenTelemetry()
            .WithTracing(tracerBuilder =>
            {
                tracerBuilder
                    .ConfigureResource(rb => rb.AddService(serviceName, null, serviceVersion))
                    .AddEntityFrameworkCoreInstrumentation()
                    .AddSqlClientInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddOtlpExporter();
            })
            .WithMetrics(meterBuilder =>
            {
                meterBuilder
                    .ConfigureResource(rb => rb.AddService(serviceName, null, serviceVersion))
                    .AddRuntimeInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddSqlClientInstrumentation()
                    .AddOtlpExporter();
            })
            .WithLogging(loggingBuilder =>
            {
                loggingBuilder
                    .ConfigureResource(rb => rb.AddService(serviceName, null, serviceVersion))
                    .AddOtlpExporter();
            });
    }
}

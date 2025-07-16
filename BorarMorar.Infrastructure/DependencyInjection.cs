using Microsoft.EntityFrameworkCore;
using System.Reflection;
using OpenTelemetry.Trace;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using BoraMorar.Imoveis.Repository;
using BoraMorar.Infrastructure.Repositories;
using BoraMorar.Infrastructure;
using BoraMorar.Cotacoes.Repository;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext();
        services.AddRepositories();
        services.AddOpenTelemetryExporter();
    }
    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IImovelRepository, ImovelRepository>();
        services.AddScoped<ICotacaoRepository, CotacaoRepository>();
    }
    private static void AddDbContext(this IServiceCollection services)
    {
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

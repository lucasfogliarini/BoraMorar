using BoraMorar.Cotacoes.Repository;
using BoraMorar.Imoveis.Repository;
using BoraMorar.Infrastructure;
using BoraMorar.Infrastructure.DomainEvents;
using BoraMorar.Infrastructure.Repositories;
using BoraMorar.Propostas.Repository;
using BorarMorar.Infrastructure.Redis;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using StackExchange.Redis;
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
    public static void AddOutputCache(this WebApplicationBuilder builder)
    {
        var redisSettings = builder.Configuration.GetSection(nameof(RedisSettings)).Get<RedisSettings>();

        builder.Services.AddOutputCache(options =>
        {
            options.AddBasePolicy(policy => policy.Expire(TimeSpan.FromSeconds(30)));
            options.AddPolicy("5", policy => policy.Expire(TimeSpan.FromSeconds(30)));
        })
        .AddStackExchangeRedisOutputCache(redisOptions =>
        {
            redisOptions.ConfigurationOptions = new ConfigurationOptions
            {
                EndPoints = { $"{redisSettings.Endpoint!}:{redisSettings.Port}" },
                Password = redisSettings.Password,
                Ssl = redisSettings.Ssl,
                AbortOnConnectFail = redisSettings.AbortOnConnectFail
            };
        });
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

using BoraMorar.WebApi;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddWebApi(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpoints();
        builder.Services.AddProblemDetails();
        builder.AddOutputCache();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.AddAll();
    }
    public static void UseWebApi(this WebApplication app)
    {
        app.UseOutputCache();//precisa ser antes do MapEndpoints
        app.MapEndpoints();        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseAll();
    }

    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        var endpointTypes = typeof(Program).Assembly
            .DefinedTypes
            .Where(type => !type.IsAbstract
                           && !type.IsInterface
                           && typeof(IEndpoint).IsAssignableFrom(type))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type));

        services.TryAddEnumerable(endpointTypes);

        return services;
    }
    public static IApplicationBuilder MapEndpoints(this WebApplication app)
    {
        IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

        foreach (IEndpoint endpoint in endpoints)
        {
            endpoint.MapEndpoint(app);
        }

        return app;
    }

    /// <summary>
    /// Esse método adiciona os middlewares mais importantes do ASP.NET Core, como logging, autorização e controllers.
    /// </summary>
    public static void AddAll(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpLogging(c =>
        {
            c.LoggingFields = AspNetCore.HttpLogging.HttpLoggingFields.All;
        });
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
    }
    /// <summary>
    /// Esse método usa os middlewares mais importantes do ASP.NET Core, como logging, autorização e controllers.
    /// </summary>
    /// <param name="builder"></param>
    public static void UseAll(this WebApplication app)
    {
        app.UseHttpLogging();
        app.UseAuthorization();
        app.MapControllers();
    }
}
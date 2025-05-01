using BoraMorar.Moradias.Crawlers;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddCrawlers(this IServiceCollection services)
    {
        services.AddScoped<ICyrelaCrawler, CyrelaCrawler>();
    }
}

using BoraMorar.Moradias.Consumers;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]

namespace BoraMorar.Moradias.Consumers
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<IRepository>());
            builder.Services.AddInfrastructure();
        }
    }
}

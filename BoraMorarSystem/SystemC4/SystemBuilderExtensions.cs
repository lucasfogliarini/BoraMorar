public static class SystemBuilderExtensions
{
    public static void AddToAspire(this IDistributedApplicationBuilder builder, SystemC4 system)
    {
        system.AddToAspire(builder);
    }
}
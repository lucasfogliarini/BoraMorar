public static class BoraMorarSystemBuilderExtensions
{
    public static BoraMorarSystem AddBoraMorarSystem(this IDistributedApplicationBuilder builder)
    {
        var boraMorarSystem = new BoraMorarSystem();
        boraMorarSystem.Add(builder);
        return boraMorarSystem;
    }
}
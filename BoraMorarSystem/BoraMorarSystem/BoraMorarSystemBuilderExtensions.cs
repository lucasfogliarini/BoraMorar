public static class BoraMorarSystemBuilderExtensions
{
    public static BoraMorarSystem AddBoraMorarSystem(this BoraKeycloakSystem boraKeycloakSystem)
    {
        var boraMorarSystem = new BoraMorarSystem(boraKeycloakSystem);
        boraMorarSystem.AddToResources();
        return boraMorarSystem;
    }
}
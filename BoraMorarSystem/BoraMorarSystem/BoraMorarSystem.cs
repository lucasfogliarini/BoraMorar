public class BoraMorarSystem() : SystemC4(SystemName)
{
    const string SystemName = nameof(BoraMorarSystem);
    public override string Url { get; set; } = "https://bora.earth/work/BoraMorar/";
    public void Add(IDistributedApplicationBuilder builder)
    {
        AddSystem(builder);
    }
}
namespace BorarMorar.Infrastructure.Redis;
public class RedisSettings
{
    public string? Endpoint { get; set; }
    public int Port { get; set; } = 13922;
    public string? Password { get; set; }
    public bool Ssl { get; set; } = false;
    public bool AbortOnConnectFail { get; set; }
}

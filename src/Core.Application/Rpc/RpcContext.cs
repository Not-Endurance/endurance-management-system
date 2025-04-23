namespace Core.Application.Rpc;

public class RpcContext : IRpcContext
{
    private readonly int? port;
    private readonly string endpoint;
    private string? localHost;
    private string remoteHost;

    public RpcContext(string remoteHost, string endpoint, int localhostPort)
    {
        if (endpoint.StartsWith("/"))
        {
            endpoint = endpoint[1..];
        }
        this.port = localhostPort;
        this.endpoint = endpoint;
		this.remoteHost = NormalizeHost(remoteHost);
#if DEBUG
		UseLocalHost = true;
# endif
    }

    public bool UseLocalHost { get; set; }
    public string? LocalHost
    {
        get => this.localHost;
        set => this.localHost = value == null ? null : NormalizeHost(value);
    }

    public string Url
        => UseLocalHost
            ? $"{RpcProtocls.Http.ToString().ToLower()}://{this.localHost}:{this.port}/{this.endpoint}"
            : $"{RpcProtocls.Https.ToString().ToLower()}://{this.remoteHost}/{this.endpoint}";

    private string NormalizeHost(string host)
    {
        if (host.EndsWith("/") || host.EndsWith(":"))
        {
            return host[..^1];
        }
        else
        {
            return host;
        }
    }
}

public interface IRpcContext
{
    bool UseLocalHost { get; set; }
    string Url { get; }
    string? LocalHost { get; set; }
}

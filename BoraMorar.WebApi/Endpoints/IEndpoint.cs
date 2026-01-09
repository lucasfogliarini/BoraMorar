namespace BoraMorar.WebApi;

public interface IEndpoint
{
    IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder app);
}
namespace BoraMorar.WebApi.Endpoints;

internal sealed class GetAccountEndpoint : IEndpoint
{
    public async Task<IResult> GetAsync(ILogger<GetAccountEndpoint> logger, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Bora!");

        return Results.Ok();
    }

    public IEndpointConventionBuilder MapEndpoint(IEndpointRouteBuilder app)
    {
        return app.MapGet($"{Routes.Accounts}", GetAsync)
           .WithTags(Routes.Accounts)
           .Produces(StatusCodes.Status200OK);
    }
}

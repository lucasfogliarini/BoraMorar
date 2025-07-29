using BoraMorar.Application;
using BoraMorar.Application.Cotacoes.Cotar;

namespace BoraMorar.WebApi;

internal sealed class CotarEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost($"{Routes.Cotacoes}/Cotar", async (
            CotarCommand command,
            ICommandHandler<CotarCommand, CotarResponse> commandHandler,
            CancellationToken cancellationToken) =>
        {
            var result = await commandHandler.Handle(command, cancellationToken);
            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Created("", result.Value);
        })
        .WithTags(Routes.Cotacoes);
    }
}

//[ApiController]
//[Route(Routes.Cotacoes)]
//[Tags(Routes.Cotacoes)]
//public class CotarControllerEndpoint(ICommandHandler<CotarCommand, CotarResponse> commandHandler) : ControllerBase
//{
//    [HttpPost("Cotar")]
//    public async Task<IActionResult> Cotar(CotarCommand command, CancellationToken cancellationToken = default)
//    {
//        var result = await commandHandler.Handle(command, cancellationToken);
//        if (result.IsFailure)
//            return BadRequest(result.Error);

//        return Created("",result.Value);
//    }
//}

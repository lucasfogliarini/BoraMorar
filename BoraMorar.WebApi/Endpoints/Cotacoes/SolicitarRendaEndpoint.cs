using BoraMorar.Application;
using BoraMorar.Application.Cotacoes.SolicitarRenda;

namespace BoraMorar.WebApi;

internal sealed class SolicitarRendaEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost($"{Routes.Cotacoes}/SolicitarRenda", async (
            SolicitarRendaCommand command,
            ICommandHandler<SolicitarRendaCommand, SolicitarRendaResponse> commandHandler,
            CancellationToken cancellationToken) =>
        {
            var result = await commandHandler.Handle(command, cancellationToken);
            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Ok(result.Value);
        })
        .WithTags(Routes.Cotacoes);
    }
}


//[ApiController]
//[Route(Routes.Cotacoes)]
//[Tags(Routes.Cotacoes)]
//public class SolicitarRendaControllerEndpoint(ICommandHandler<SolicitarRendaCommand, SolicitarRendaResponse> commandHandler) : ControllerBase
//{
//    [HttpPost(nameof(SolicitarRenda))]
//    public async Task<IActionResult> SolicitarRenda(SolicitarRendaCommand command, CancellationToken cancellationToken = default)
//    {
//        var result = await commandHandler.Handle(command, cancellationToken);
//        if (result.IsFailure)
//            return BadRequest(result.Error);

//        return Ok(result.Value);
//    }
//}

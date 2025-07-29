using BoraMorar.Application;
using BoraMorar.Application.Cotacoes.CalcularPrestacoes;

namespace BoraMorar.WebApi;

internal sealed class CalcularPrestacoesEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost($"{Routes.Cotacoes}/CalcularPrestacoes", async (
            CalcularPrestacoesCommand command,
            ICommandHandler<CalcularPrestacoesCommand, CalcularPrestacoesResponse> commandHandler,
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
//public class CalcularPrestacoesControllerEndpoint(ICommandHandler<CalcularPrestacoesCommand, CalcularPrestacoesResponse> commandHandler) : ControllerBase
//{
//    [HttpPost(nameof(CalcularPrestacoes))]
//    public async Task<IActionResult> CalcularPrestacoes(CalcularPrestacoesCommand command, CancellationToken cancellationToken = default)
//    {
//        var result = await commandHandler.Handle(command, cancellationToken);
//        if (result.IsFailure)
//            return BadRequest(result.Error);

//        return Ok(result.Value);
//    }
//}

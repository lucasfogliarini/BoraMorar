using BoraMorar.Application;
using BoraMorar.Application.Cotacoes.AprovarCotacao;

namespace BoraMorar.WebApi;

internal sealed class AprovarCotacaoEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost($"{Routes.Cotacoes}/AprovarCotacao", async (
            AprovarCotacaoCommand command,
            ICommandHandler<AprovarCotacaoCommand, AprovarCotacaoResponse> commandHandler,
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
//public class AprovarCotacaoControlerEndpoint(ICommandHandler<AprovarCotacaoCommand, AprovarCotacaoResponse> commandHandler) : ControllerBase
//{
//    [HttpPost("AprovarCotacao")]
//    public async Task<IActionResult> AprovarCotacao(AprovarCotacaoCommand command, CancellationToken cancellationToken = default)
//    {
//        var result = await commandHandler.Handle(command, cancellationToken);
//        if (result.IsFailure)
//            return BadRequest(result.Error);

//        return Ok(result.Value);
//    }
//}



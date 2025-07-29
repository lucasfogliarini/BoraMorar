using BoraMorar.Application;
using BoraMorar.Application.Cotacoes.BuscarCotacao;

namespace BoraMorar.WebApi;

internal sealed class BuscarCotacaoEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet($"{Routes.Cotacoes}/{{id}}", async (
            int id,
            IQueryHandler<BuscarCotacaoQuery, BuscarCotacaoResponse> queryHandler,
            CancellationToken cancellationToken) =>
        {
            var result = await queryHandler.Handle(new BuscarCotacaoQuery(id), cancellationToken);
            if (result.IsFailure)
                return Results.NotFound(result.Error);

            return Results.Ok(result.Value);
        })
        .WithTags(Routes.Cotacoes)
        .CacheOutput();
    }
}

//[ApiController]
//[Route(Routes.Cotacoes)]
//[Tags(Routes.Cotacoes)]
//public class BuscarCotacaoControllerEndpoint(IQueryHandler<BuscarCotacaoQuery, BuscarCotacaoResponse> queryHandler) : ControllerBase
//{
//    [HttpGet("{id}")]
//    public async Task<IActionResult> Buscar(int id, CancellationToken cancellationToken)
//    {
//        var result = await queryHandler.Handle(new BuscarCotacaoQuery(id), cancellationToken);
//        if (result.IsFailure)
//            return NotFound(result.Error);

//        return Ok(result.Value);
//    }
//}

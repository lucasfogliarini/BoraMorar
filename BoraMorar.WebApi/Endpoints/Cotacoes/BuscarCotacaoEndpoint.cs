using BoraMorar.Application;
using BoraMorar.Application.Cotacoes.BuscarCotacao;
using Microsoft.AspNetCore.Mvc;

namespace BoraMorar.WebApi;

[ApiController]
[Route(Routes.Cotacoes)]
public class BuscarCotacaoEndpoint(IQueryHandler<BuscarCotacaoQuery, BuscarCotacaoResponse> queryHandler) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Buscar(int id, CancellationToken cancellationToken)
    {
        var result = await queryHandler.Handle(new BuscarCotacaoQuery(id), cancellationToken);
        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }
}

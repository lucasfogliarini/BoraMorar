using BoraMorar.Application;
using BoraMorar.Application.Cotacoes.AprovarCotacao;
using Microsoft.AspNetCore.Mvc;

namespace BoraMorar.WebApi;

[ApiController]
[Route(Routes.Cotacoes)]
[Tags(Routes.Cotacoes)]
public class AprovarCotacaoEndpoint(ICommandHandler<AprovarCotacaoCommand, AprovarCotacaoResponse> commandHandler) : ControllerBase
{
    [HttpPost("AprovarCotacao")]
    public async Task<IActionResult> AprovarCotacao(AprovarCotacaoCommand command, CancellationToken cancellationToken = default)
    {
        var result = await commandHandler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}

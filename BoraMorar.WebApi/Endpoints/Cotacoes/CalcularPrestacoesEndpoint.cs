using BoraMorar.Application;
using BoraMorar.Application.Cotacoes.CalcularPrestacoes;
using Microsoft.AspNetCore.Mvc;

namespace BoraMorar.WebApi;

[ApiController]
[Route(Routes.Cotacoes)]
[Tags(Routes.Cotacoes)]
public class CalcularPrestacoesEndpoint(ICommandHandler<CalcularPrestacoesCommand, CalcularPrestacoesResponse> commandHandler) : ControllerBase
{
    [HttpPost(nameof(CalcularPrestacoes))]
    public async Task<IActionResult> CalcularPrestacoes(CalcularPrestacoesCommand command, CancellationToken cancellationToken = default)
    {
        var result = await commandHandler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}

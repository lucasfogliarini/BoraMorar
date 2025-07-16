using BoraMorar.Application;
using BoraMorar.Application.Cotacoes.SolicitarRenda;
using Microsoft.AspNetCore.Mvc;

namespace BoraMorar.WebApi;

[ApiController]
[Route(Routes.Cotacoes)]
public class SolicitarRendaEndpoint(ICommandHandler<SolicitarRendaCommand, SolicitarRendaResponse> commandHandler) : ControllerBase
{
    [HttpPost("{id}/SolicitarRenda")]
    public async Task<IActionResult> SolicitarRenda(SolicitarRendaCommand command, CancellationToken cancellationToken = default)
    {
        var result = await commandHandler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}

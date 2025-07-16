using BoraMorar.Application;
using BoraMorar.Application.Cotacoes.InformarCompromissoFinanceiro;
using Microsoft.AspNetCore.Mvc;

namespace BoraMorar.WebApi;

[ApiController]
[Route(Routes.Cotacoes)]
public class InformarCompromissoFinanceiroEndpoint(ICommandHandler<InformarCompromissoFinanceiroCommand, InformarCompromissoFinanceiroResponse> commandHandler) : ControllerBase
{
    [HttpPost("{id}/InformarCompromissoFinanceiro")]
    public async Task<IActionResult> SolicitarRenda(InformarCompromissoFinanceiroCommand command, CancellationToken cancellationToken = default)
    {
        var result = await commandHandler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }
}

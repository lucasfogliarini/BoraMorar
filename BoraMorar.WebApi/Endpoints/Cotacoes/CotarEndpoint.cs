using BoraMorar.Application;
using BoraMorar.Application.Cotacoes.Cotar;
using Microsoft.AspNetCore.Mvc;

namespace BoraMorar.WebApi;

[ApiController]
[Route(Routes.Cotacoes)]
[Tags(Routes.Cotacoes)]
public class CotarEndpoint(ICommandHandler<CotarCommand, CotarResponse> commandHandler) : ControllerBase
{
    [HttpPost("Cotar")]
    public async Task<IActionResult> Cotar(CotarCommand command, CancellationToken cancellationToken = default)
    {
        var result = await commandHandler.Handle(command, cancellationToken);
        if (result.IsFailure)
            return BadRequest(result.Error);

        return Created("",result.Value);
    }
}

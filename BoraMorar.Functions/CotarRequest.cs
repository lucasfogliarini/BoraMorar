using BoraMorar.Application;
using BoraMorar.Application.Cotacoes.Cotar;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace BoraMorar.Functions;

public class CotarRequest(ICommandHandler<CotarCommand, CotarResponse> commandHandler, ILogger<CotarRequest> logger)
{
    [Function(nameof(CotarRequest))]
    public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "post", Route = "cotacoes/cotar")] CotarCommand cotarCommand)
    {
        var cotarResult = await commandHandler.Handle(cotarCommand);
        if (cotarResult.IsFailure)
        {
            return new NotFoundObjectResult(cotarResult.Error);
        }

        return new OkObjectResult(cotarResult.Value);
    }
}
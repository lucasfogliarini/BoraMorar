using BoraMorar.Application;
using BoraMorar.Application.Cotacoes.InformarCompromissoFinanceiro;

namespace BoraMorar.WebApi;

internal sealed class InformarCompromissoFinanceiroEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost($"{Routes.Cotacoes}/InformarCompromissoFinanceiro", async (
            InformarCompromissoFinanceiroCommand command,
            ICommandHandler<InformarCompromissoFinanceiroCommand, InformarCompromissoFinanceiroResponse> commandHandler,
            CancellationToken cancellationToken) =>
        {
            var result = await commandHandler.Handle(command, cancellationToken);
            if (result.IsFailure)
                return Results.BadRequest(result.Error);

            return Results.Created("", result.Value);
        })
        .WithTags(Routes.Cotacoes);
    }
}


//[ApiController]
//[Route(Routes.Cotacoes)]
//[Tags(Routes.Cotacoes)]
//public class InformarCompromissoFinanceiroControllerEndpoint(ICommandHandler<InformarCompromissoFinanceiroCommand, InformarCompromissoFinanceiroResponse> commandHandler) : ControllerBase
//{
//    [HttpPost(nameof(InformarCompromissoFinanceiro))]
//    public async Task<IActionResult> InformarCompromissoFinanceiro(InformarCompromissoFinanceiroCommand command, CancellationToken cancellationToken = default)
//    {
//        var result = await commandHandler.Handle(command, cancellationToken);
//        if (result.IsFailure)
//            return BadRequest(result.Error);

//        return Ok(result.Value);
//    }
//}

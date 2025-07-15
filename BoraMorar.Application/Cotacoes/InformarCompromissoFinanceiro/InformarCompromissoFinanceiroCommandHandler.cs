using BoraMorar.Cotacoes.Repository;
using CSharpFunctionalExtensions;

namespace BoraMorar.Application.Cotacoes.InformarCompromissoFinanceiro;

public class InformarCompromissoFinanceiroCommandHandler(ICotacaoRepository repository) : ICommandHandler<InformarCompromissoFinanceiroCommand, InformarCompromissoFinanceiroResponse>
{
    public async Task<Result<InformarCompromissoFinanceiroResponse>> Handle(InformarCompromissoFinanceiroCommand command, CancellationToken cancellationToken = default)
    {
        Result<Cotacao> result = await repository.FindAsync(command.Id);
        return result
            .EnsureNotNull("Cotação não encontrada.")
            .Tap(c =>
            {
                c.InformarCompromissoFinanceiro(command.RendaBrutaMensal, command.PrazoPretendido);
                repository.CommitScope.Commit();
            })
            .MapTry(c => new InformarCompromissoFinanceiroResponse(c.Id, c.Status, c.DataCompromissoFinanceiroInformado));
    }
}

using BoraMorar.Cotacoes.Repository;
using CSharpFunctionalExtensions;

namespace BoraMorar.Application.Cotacoes.SolicitarRenda;

public class SolicitarRendaCommandHandler(ICotacaoRepository repository) : ICommandHandler<SolicitarRendaCommand, SolicitarRendaResponse>
{
    public async Task<Result<SolicitarRendaResponse>> Handle(SolicitarRendaCommand command, CancellationToken cancellationToken = default)
    {
        Result<Cotacao> result = await repository.FindAsync(command.Id);
        return result
            .EnsureNotNull("Cotação não encontrada.")
            .Tap(c =>
            {
                c.SolicitarRenda(command.CorretorId);
                repository.CommitScope.Commit(cancellationToken);
            })
            .MapTry(c => new SolicitarRendaResponse(c.Id, c.Status, c.DataRendaSolicitada));
    }
}

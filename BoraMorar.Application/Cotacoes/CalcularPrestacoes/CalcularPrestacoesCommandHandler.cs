using BoraMorar.Cotacoes;
using BoraMorar.Cotacoes.Repository;
using CSharpFunctionalExtensions;

namespace BoraMorar.Application.Cotacoes.CalcularPrestacoes;

public class CalcularPrestacoesCommandHandler(ICotacaoRepository repository) : ICommandHandler<CalcularPrestacoesCommand, CalcularPrestacoesResponse>
{
    public async Task<Result<CalcularPrestacoesResponse>> Handle(CalcularPrestacoesCommand command, CancellationToken cancellationToken = default)
    {
        Result<Cotacao> result = await repository.FindAsync(command.Id);
        return result
            .EnsureNotNull("Cotação não encontrada.")
            .Tap(c =>
            {
                c.CalcularPrestacoes(command.TaxaJuros, command.PrazoMaximo);
                repository.CommitScope.Commit(cancellationToken);
            })
            .MapTry(c => new CalcularPrestacoesResponse(c.Id, c.Status, c.DataPrestacoesCalculadas, c.PrestacaoPrazoPretendido, c.PrestacaoPrazoMaximo));
    }
}

public record CalcularPrestacoesCommand(int Id, decimal TaxaJuros, int PrazoMaximo) : ICommand<CalcularPrestacoesResponse>;

public record CalcularPrestacoesResponse(int Id, CotacaoStatus Status, DateTime DataPrestacoesCalculadas, decimal PrestacaoPrazoPretendido, decimal PrestacaoPrazoMaximo);

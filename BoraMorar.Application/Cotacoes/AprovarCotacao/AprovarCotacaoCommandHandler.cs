using BoraMorar.Cotacoes;
using BoraMorar.Cotacoes.Repository;
using CSharpFunctionalExtensions;

namespace BoraMorar.Application.Cotacoes.AprovarCotacao;

public class AprovarCotacaoCommandHandler(ICotacaoRepository repository) : ICommandHandler<AprovarCotacaoCommand, AprovarCotacaoResponse>
{
    public async Task<Result<AprovarCotacaoResponse>> Handle(AprovarCotacaoCommand command, CancellationToken cancellationToken = default)
    {
        Result<Cotacao> result = await repository.FindAsync(command.Id);
        return result
            .EnsureNotNull("Cotação não encontrada.")
            .Tap(c =>
            {
                c.AprovarCotacao();
                repository.CommitScope.Commit();
            })
            .MapTry(c => new AprovarCotacaoResponse(c.Id, c.Status, c.DataCotacaoAprovada));
    }
}

public record AprovarCotacaoCommand(int Id, decimal TaxaJuros, int PrazoMaximo) : ICommand<AprovarCotacaoResponse>;

public record AprovarCotacaoResponse(int Id, CotacaoStatus Status, DateTime DataCotacaoAprovada);

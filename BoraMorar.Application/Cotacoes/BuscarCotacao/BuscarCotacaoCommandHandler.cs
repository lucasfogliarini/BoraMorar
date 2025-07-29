using BoraMorar.Cotacoes.Repository;
using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.ValueTasks;

namespace BoraMorar.Application.Cotacoes.BuscarCotacao;

public class BuscarCotacaoCommandHandler(ICotacaoRepository repository) : IQueryHandler<BuscarCotacaoQuery, BuscarCotacaoResponse>
{
    public async Task<Result<BuscarCotacaoResponse>> Handle(BuscarCotacaoQuery query, CancellationToken cancellationToken = default)
    {
        await Task.Delay(2000, cancellationToken);

        return await Result
            .Try(() => repository.FindAsync(query.Id))
            .EnsureNotNull("Cotação não encontrada.")
            .MapTry(c => new BuscarCotacaoResponse(c.Id, c.Numero, c.DataCotacaoSolicitada));
    }
}

public record BuscarCotacaoQuery(int Id) : IQuery<BuscarCotacaoResponse>;

public record BuscarCotacaoResponse(int Id, string Numero, DateTime DataCotacaoSolicitada);

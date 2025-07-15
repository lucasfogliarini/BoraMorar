using BoraMorar.Cotacoes.Repository;
using CSharpFunctionalExtensions;

namespace BoraMorar.Application.Cotacoes.Cotar;

public class CotarCommandHandler(ICotacaoRepository repository) : ICommandHandler<CotarCommand, CotarResponse>
{
    public async Task<Result<CotarResponse>> Handle(CotarCommand command, CancellationToken cancellationToken = default)
    {
        return await Result
            .Try(async () =>
            {
                var cotacao = new Cotacao(command.ClienteId, command.TipoDoBem, command.Preco);
                repository.Add(cotacao);
                await repository.CommitScope.CommitAsync(cancellationToken);

                return new CotarResponse(cotacao.Id, cotacao.Numero, cotacao.Status, cotacao.DataCotacaoSolicitada);
            });
    }
}

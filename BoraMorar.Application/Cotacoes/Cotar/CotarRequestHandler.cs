namespace BoraMorar.Application.Cotacoes.Cotar;

public class CotarCommkandHandler(ICotacaoRepository repository) : ICommandHandler<CotarCommand, CotarResponse>
{
    public async Task<CotarResponse> Handle(CotarCommand command, CancellationToken cancellationToken)
    {
        var cotacao = new Cotacao(command.ClienteId, command.TipoDoBem, command.Preco);
        repository.Add(cotacao);
        await repository.CommitScope.CommitAsync();
        return new CotarResponse(cotacao.Id, cotacao.Numero, cotacao.Status, cotacao.DataCotacaoSolicitada);
    }
}

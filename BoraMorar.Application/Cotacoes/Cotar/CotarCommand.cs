namespace BoraMorar.Application.Cotacoes.Cotar;

public record CotarCommand(int ClienteId, TipoDoBem TipoDoBem, decimal Preco) : ICommand<CotarResponse>;

public record CotarResponse(int Id, string Numero, CotacaoStatus Status, DateTime DataCotacaoSolicitada);

namespace BoraMorar.Application.Cotacoes.Cotar;

public record CotarResponse(int Id, string Numero, CotacaoStatus Status, DateTime DataCotacaoSolicitada);

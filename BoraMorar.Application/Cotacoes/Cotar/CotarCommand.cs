using BoraMorar.Cotacoes;

namespace BoraMorar.Application.Cotacoes.Cotar;

public record CotarCommand(int ClienteId, TipoDoBem TipoDoBem, decimal Preco) : ICommand<CotarResponse>;

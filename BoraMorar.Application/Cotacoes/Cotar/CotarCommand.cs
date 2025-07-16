using BoraMorar.Cotacoes;

namespace BoraMorar.Application.Cotacoes.Cotar;

public class CotarCommand : ICommand<CotarResponse>
{
    public int ClienteId { get; init; }
    public TipoDoBem TipoDoBem { get; init; }
    public decimal Preco { get; init; }
}


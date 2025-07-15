using BoraMorar.Cotacoes;

namespace BoraMorar.Application.Cotacoes.InformarCompromissoFinanceiro;

public record InformarCompromissoFinanceiroResponse(int Id, CotacaoStatus Status, DateTime DataCompromissoFinanceiroInformado);

namespace BoraMorar.Application.Cotacoes.InformarCompromissoFinanceiro;

public record InformarCompromissoFinanceiroCommand(int Id, decimal RendaBrutaMensal, int PrazoPretendido) : ICommand<InformarCompromissoFinanceiroResponse>;

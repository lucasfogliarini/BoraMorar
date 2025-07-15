namespace BoraMorar.Application.Cotacoes.SolicitarRenda;

public record SolicitarRendaCommand(int Id, int CorretorId) : ICommand<SolicitarRendaResponse>;

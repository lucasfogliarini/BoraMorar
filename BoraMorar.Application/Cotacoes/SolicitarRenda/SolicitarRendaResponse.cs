using BoraMorar.Cotacoes;

namespace BoraMorar.Application.Cotacoes.SolicitarRenda;

public record SolicitarRendaResponse(int Id, CotacaoStatus Status, DateTime DataRendaSolicitada);

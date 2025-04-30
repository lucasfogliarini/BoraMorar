namespace BoraMorar.Cotacoes.DomainEvents
{
    public record CotacaoStatusChangedDomainEvent(long Id, CotacaoStatus Status, DateTime ChangedAt) : IDomainEvent { }
}

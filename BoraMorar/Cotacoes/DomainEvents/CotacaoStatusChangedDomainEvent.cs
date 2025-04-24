namespace BoraMorar.Cotacoes.DomainEvents
{
    public record CotacaoStatusChangedDomainEvent(int Id, CotacaoStatus Status, DateTime ChangedAt) : IDomainEvent { }
}

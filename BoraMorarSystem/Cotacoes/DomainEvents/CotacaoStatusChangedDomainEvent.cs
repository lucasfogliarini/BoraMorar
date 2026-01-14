namespace BoraMorar.Cotacoes.DomainEvents
{
    public class CotacaoStatusChangedDomainEvent(long id, CotacaoStatus status) : DomainEvent 
    { 
        public long CotacaoId { get; } = id;
        public CotacaoStatus NewStatus { get; } = status;
    }
}

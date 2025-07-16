using BoraMorar.Infrastructure;
using Microsoft.Extensions.Logging;

namespace BoraMorar.Cotacoes.DomainEvents;

public class CotacaoStatusChangedDomainEventHandler(IProducer producer, ILogger<CotacaoStatusChangedDomainEventHandler> logger) : IDomainEventHandler<CotacaoStatusChangedDomainEvent>
{
    public async Task HandleAsync(CotacaoStatusChangedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("{id}, {status}, {occurredOn}", domainEvent.CotacaoId, domainEvent.NewStatus, domainEvent.OccurredOn);
        await producer.ProduceAsync("cotacao-status-changed", domainEvent);
    }
}



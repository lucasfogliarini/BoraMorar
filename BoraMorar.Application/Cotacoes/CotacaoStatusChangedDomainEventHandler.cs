using BoraMorar.Infrastructure;
using Microsoft.Extensions.Logging;

namespace BoraMorar.Cotacoes.DomainEvents;

public class CotacaoStatusChangedDomainEventHandler(IProducer producer, ILogger<CotacaoStatusChangedDomainEventHandler> logger) : IDomainEventHandler<CotacaoStatusChangedDomainEvent>
{
    public async Task HandleAsync(CotacaoStatusChangedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("{id}, {status}, {changedAt}", domainEvent.Id, domainEvent.Status, domainEvent.ChangedAt);
        await producer.ProduceAsync("cotacao-status-changed", domainEvent);
    }
}



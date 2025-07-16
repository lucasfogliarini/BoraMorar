using BoraMorar.Cotacoes;
using BoraMorar.Cotacoes.DomainEvents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Kafka;
using Microsoft.Extensions.Logging;

namespace BoraMorar.Functions;

public class CotacaoStatusChangedConsumer(ILogger<CotacaoStatusChangedConsumer> logger)
{
    [FunctionName(nameof(CotacaoStatusChangedConsumer))]
    public void Run(
        [KafkaTrigger("%BrokerList%",
                      "cotacao-status-changed",
                      ConsumerGroup = nameof(CotacaoStatusChangedConsumer))]
                      CotacaoStatusChangedDomainEvent domainEvent)
    {
        logger.LogInformation("Received domain event: {id}, {status}, {changedAt}", domainEvent.Id, domainEvent.Status, domainEvent.ChangedAt);
        if (domainEvent.Status != CotacaoStatus.CotacaoAprovada)
            return;
    }
}

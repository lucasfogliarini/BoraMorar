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
        logger.LogInformation("Received domain event: {id}, {status}, {occurredOn}", domainEvent.CotacaoId, domainEvent.NewStatus, domainEvent.OccurredOn);
        if (domainEvent.NewStatus != CotacaoStatus.CotacaoAprovada)
            return;
    }
}

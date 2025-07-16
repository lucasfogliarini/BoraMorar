using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.Kafka;

namespace BoraMorar.Functions;

public class CotacaoStatusChangedConsumer(IMediator mediator)
{
    [Function(nameof(CotacaoStatusChangedConsumer))]
    public void Run(
        [KafkaTrigger("%BrokerList%",
                      "cotacao-status-changed",
                      ConsumerGroup = nameof(CotacaoStatusChangedConsumer))]
                      CotacaoStatusChangedDomainEvent domainEvent)
    {
        if(domainEvent.Status != CotacaoStatus.CotacaoAprovada)
            return;
        var gerarProposta = new GerarPropostaRequest(domainEvent.Id);
        mediator.Send(gerarProposta);
    }
}

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BoraMorar.Infrastructure.DomainEvents;

public class DomainEventsDispatcher(IServiceProvider serviceProvider)
{
    public async Task DispatchAsync(IEnumerable<IDomainEvent> events, CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in events)
        {
            var eventType = domainEvent.GetType();

            // Busca o método genérico DispatchTyped<TEvent> e fecha com o tipo correto
            var method = typeof(DomainEventsDispatcher)
                .GetMethod(nameof(DispatchTyped), BindingFlags.NonPublic | BindingFlags.Instance)!
                .MakeGenericMethod(eventType);

            // Executa o método genérico com o evento e o token de cancelamento
            await (Task)method.Invoke(this, [domainEvent, cancellationToken])!;
        }
    }
    private async Task DispatchTyped<TEvent>(TEvent domainEvent, CancellationToken cancellationToken) where TEvent : IDomainEvent
    {
        var handlers = serviceProvider.GetServices<IDomainEventHandler<TEvent>>();

        foreach (var handler in handlers)
        {
            await handler.HandleAsync(domainEvent, cancellationToken);
        }
    }
}

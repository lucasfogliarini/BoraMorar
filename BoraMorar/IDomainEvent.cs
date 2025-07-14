namespace BoraMorar;

/// <summary>
/// Representa um evento de domínio que descreve algo que aconteceu no sistema.
/// Cor no EventStorming: <b>Laranja</b> (Domain Event).
/// </summary>
public interface IDomainEvent;

public interface IDomainEventHandler<in TEvent> where TEvent : IDomainEvent
{
    Task HandleAsync(TEvent domainEvent, CancellationToken cancellationToken);
}


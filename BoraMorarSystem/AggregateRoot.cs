using CSharpFunctionalExtensions;

namespace BoraMorar
{
    /// <summary>
    /// Representa a raiz de um agregado, sendo a única entrada para modificar o estado interno do agregado.
    /// É responsável por garantir as invariantes do domínio.
    /// Cor no EventStorming: <b>Roxo</b>.
    /// </summary>
    public abstract class AggregateRoot : Entity<int>
    {
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        private List<IDomainEvent> _domainEvents = [];
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents ??= [];
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        protected void CreatedNow()
        {
            CreatedAt = DateTime.Now;
        }
        protected void UpdatedNow()
        {
            UpdatedAt = DateTime.Now;
        }

        protected string GerarNumero(string prefix) => $"{prefix}-{DateTime.Now:yyyyMMddHHmmss}-{new Random().Next(1000, 9999)}";
    }
}

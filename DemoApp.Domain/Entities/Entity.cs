using DemoApp.Domain.Events;

namespace DemoApp.Domain.Entities
{
    public abstract class Entity : IHasDomainEvents
    {
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifedOn { get; set; }
        private readonly List<IDomainEvent> _domainEvents = new();

        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public Entity()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;
            ModifedOn = DateTime.UtcNow;
        }

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}


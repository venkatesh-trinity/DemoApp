namespace DemoApp.Domain.Events
{
	public interface IHasDomainEvents
	{
        public IReadOnlyList<IDomainEvent> DomainEvents { get; }

        public void ClearDomainEvents();
    }
}


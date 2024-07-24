using DemoApp.Domain.Entities;

namespace DemoApp.Domain.Events
{
	public record BankAccountCreated(BankAccount account): IDomainEvent;
}


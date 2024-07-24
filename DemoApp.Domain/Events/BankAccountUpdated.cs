using DemoApp.Domain.Entities;

namespace DemoApp.Domain.Events
{
    public record BankAccountUpdated(BankAccount account): IDomainEvent;
}


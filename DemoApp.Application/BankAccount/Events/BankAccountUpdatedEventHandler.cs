using DemoApp.Domain.Events;
using MediatR;

namespace DemoApp.Application.BankAccount.Events
{
    public class BankAccountUpdatedEventHandler: INotificationHandler<BankAccountUpdated>
	{
		public BankAccountUpdatedEventHandler()
		{
		}

        public Task Handle(BankAccountUpdated notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}


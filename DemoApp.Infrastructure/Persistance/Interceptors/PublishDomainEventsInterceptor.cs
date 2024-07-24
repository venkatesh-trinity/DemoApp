using DemoApp.Domain.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DemoApp.Infrastructure.Persistance.Interceptors
{
    public class PublishDomainEventsInterceptor: SaveChangesInterceptor
	{
        private readonly IPublisher _publisher;

        public PublishDomainEventsInterceptor(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public async override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData,
            int result, CancellationToken cancellationToken = default)
        {
            await PublishDomainEvents(eventData.Context);
            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        private async Task PublishDomainEvents(DbContext? dbContext)
        {
            if (dbContext is null)
            {
                return;
            }

            var entitiesWithDomainEvents = dbContext.ChangeTracker.Entries<IHasDomainEvents>()
                            .Where(entry => entry.Entity.DomainEvents.Any())
                            .Select(entry => entry.Entity)
                            .ToList();

            var domainEvents = entitiesWithDomainEvents.SelectMany(entry => entry.DomainEvents).ToList();

            //clear domainn events;
            entitiesWithDomainEvents.ForEach(entity => entity.ClearDomainEvents());

            //publish domain events
            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent);
            }
        }
    }
}


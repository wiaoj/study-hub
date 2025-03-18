using CQRS.EventSourcing.Core.Domain;
using CQRS.EventSourcing.Core.Events;
using CQRS.EventSourcing.Core.Exceptions;
using CQRS.EventSourcing.Core.Infrastructure;
using CQRS.EventSourcing.Core.Producers;
using SocialMedia.Posts.Commands.Domain.Aggregates;
using SocialMedia.Posts.Common.Extensions;

namespace SocialMedia.Posts.Commands.Infrastructure.Stores;
public class EventStore(IEventStoreRepository eventStoreRepository, IEventProducer eventProducer) : IEventStore {
    public async Task<List<Guid>> GetAggregateIdsAsync<TId>(CancellationToken cancellationToken) where TId : AggregateRootId {
        List<EventModel> eventStream = await eventStoreRepository.FindAllAsync(cancellationToken);

        if(eventStream.IsNull() || eventStream.IsZero()) {
            throw new ArgumentNullException(nameof(eventStream), "Could not retrieve event stream from the event store!");
        }

        return eventStream.Select(x => x.AggregateIdentifier).Distinct().ToList();
    }

    public async Task<List<BaseEvent>> GetEventsAsync<TId>(TId aggregateId, CancellationToken cancellationToken) where TId : AggregateRootId {
        List<EventModel> eventStream = await eventStoreRepository.FindByAggregateId(aggregateId, cancellationToken);

        return eventStream.IsNull() || eventStream.IsZero()
            ? throw new AggregateNotFoundException("Incorrect post ID provided!")
            : eventStream.OrderBy(x => x.Version).Select(x => x.EventData).ToList();
    }

    public async Task SaveEventsAsync<TId>(TId aggregateId,
                                           IEnumerable<BaseEvent> events,
                                           Int32 expectedVersion,
                                           CancellationToken cancellationToken) where TId : AggregateRootId {
        List<EventModel> eventStream = await eventStoreRepository.FindByAggregateId(aggregateId, cancellationToken);

        if(expectedVersion != -1 && eventStream[^1].Version != expectedVersion)
            throw new ConcurrencyException();

        Int32 version = expectedVersion;

        foreach(BaseEvent @event in events) {
            @event.IncreaseVersion();
            String eventType = @event.GetType().Name;
            EventModel eventModel = new() {
                TimeStamp = DateTime.UtcNow, //TODO
                AggregateIdentifier = aggregateId.Value,
                AggregateType = nameof(PostAggregate),
                Version = version,
                EventType = eventType,
                EventData = @event
            };

            await eventStoreRepository.SaveAsync(eventModel, cancellationToken);

            //TODO
            await eventProducer.ProduceAsync("socialMedia", @event);
        }
    }
}
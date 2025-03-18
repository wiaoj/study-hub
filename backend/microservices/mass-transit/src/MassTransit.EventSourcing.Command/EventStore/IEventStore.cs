using MassTransit.EventSourcing.Command.Domain;
using MassTransit.EventSourcing.Command.EventPublisher;
using MassTransit.EventSourcing.Command.Events;

namespace MassTransit.EventSourcing.Command.EventStore;
public interface IEventStore {
    T GetAggregate<T>(String id) where T : PostAggregate;
    Task SaveEventsAsync<T>(IEnumerable<T> events) where T : BaseEvent;
}
public class InMemoryEventStore : IEventStore {
    private readonly IEventStoreRepository eventStoreRepository;
    private readonly IEventPublisher eventPublisher;

    public InMemoryEventStore(IEventStoreRepository eventStoreRepository, IEventPublisher eventPublisher) {
        this.eventStoreRepository = eventStoreRepository;
        this.eventPublisher = eventPublisher;
    }
    public T GetAggregate<T>(String id) where T : PostAggregate {
        IEnumerable<PostCreatedEvent> events = this.eventStoreRepository.GetEventsAsync<PostCreatedEvent>(id).Result;
        if(events == null || !events.Any())
            return null;

        // Create a new instance of the aggregate
        PostAggregate aggregate = PostAggregate.Empty;

        // Apply each event to the aggregate to reconstruct its state
        foreach(PostCreatedEvent @event in events) {
            aggregate.Apply(@event);
        }

        return (T)Convert.ChangeType(aggregate, typeof(T));
    }

    public async Task SaveEventsAsync<T>(IEnumerable<T> events) where T : BaseEvent {
        await this.eventStoreRepository.SaveAsync(events);
        await this.eventPublisher.PublishAsync(events);
    }
}
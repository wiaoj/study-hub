using MassTransit.EventSourcing.Command.Events;

namespace MassTransit.EventSourcing.Command.EventPublisher;
public interface IEventPublisher {
    Task PublishAsync<T>(IEnumerable<T> events) where T : BaseEvent;
}

public class MassTransitEventPublisher(IPublishEndpoint publishEndpoint) : IEventPublisher {
    public async Task PublishAsync<T>(IEnumerable<T> events) where T : BaseEvent {
        foreach(T @event in events) {
            await publishEndpoint.Publish(@event);
        }
    }
}
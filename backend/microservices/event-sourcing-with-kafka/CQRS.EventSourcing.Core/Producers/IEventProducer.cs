using CQRS.EventSourcing.Core.Events;

namespace CQRS.EventSourcing.Core.Producers;
public interface IEventProducer {
    Task ProduceAsync<T>(String topic, T @event) where T : BaseEvent;
}
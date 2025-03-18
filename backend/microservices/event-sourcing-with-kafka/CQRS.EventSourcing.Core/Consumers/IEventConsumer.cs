namespace CQRS.EventSourcing.Core.Consumers;
public interface IEventConsumer {
    void Consume(String topic);
}
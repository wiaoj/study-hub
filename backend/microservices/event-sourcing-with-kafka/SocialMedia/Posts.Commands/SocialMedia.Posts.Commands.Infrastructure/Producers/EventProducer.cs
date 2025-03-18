using Confluent.Kafka;
using CQRS.EventSourcing.Core.Events;
using CQRS.EventSourcing.Core.Producers;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace SocialMedia.Posts.Commands.Infrastructure.Producers;
public class EventProducer : IEventProducer {
    private readonly ProducerConfig producerConfig;

    public EventProducer(IOptions<ProducerConfig> config) {
        this.producerConfig = config.Value;
    }

    public async Task ProduceAsync<T>(String topic, T @event) where T : BaseEvent {
        using IProducer<String, String> producer = new ProducerBuilder<String, String>(this.producerConfig)
            .SetKeySerializer(Serializers.Utf8)
            .SetValueSerializer(Serializers.Utf8)
            .Build();

        Message<String, String> eventMessage = new() {
            Key = Guid.NewGuid().ToString(),
            Value = JsonSerializer.Serialize(@event, @event.GetType())
        };

        DeliveryResult<String, String> deliveryResult = await producer.ProduceAsync(topic, eventMessage);

        if(deliveryResult.Status == PersistenceStatus.NotPersisted)
            throw new Exception($"Could not produce {@event.GetType().Name} message to topic - {topic} due to the following reason: {deliveryResult.Message}.");
    }
}
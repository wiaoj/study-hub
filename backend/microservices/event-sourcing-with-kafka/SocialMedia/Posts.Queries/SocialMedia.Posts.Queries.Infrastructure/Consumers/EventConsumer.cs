using Confluent.Kafka;
using CQRS.EventSourcing.Core.Consumers;
using CQRS.EventSourcing.Core.Events;
using Microsoft.Extensions.Options;
using SocialMedia.Posts.Queries.Infrastructure.Converters;
using SocialMedia.Posts.Queries.Infrastructure.Handlers;
using System.Reflection;
using System.Text.Json;

namespace SocialMedia.Posts.Queries.Infrastructure.Consumers;
public class EventConsumer : IEventConsumer {
    private readonly ConsumerConfig config;
    private readonly IEventHandler eventHandler;

    public EventConsumer(IOptions<ConsumerConfig> config, IEventHandler eventHandler) {
        this.config = config.Value;
        this.eventHandler = eventHandler;
    }

    public void Consume(String topic) {
        using IConsumer<String, String> consumer = new ConsumerBuilder<String, String>(this.config)
                .SetKeyDeserializer(Deserializers.Utf8)
                .SetValueDeserializer(Deserializers.Utf8)
                .Build();

        consumer.Subscribe(topic);

        while(true) {
            ConsumeResult<String, String> consumeResult = consumer.Consume();

            if(consumeResult?.Message == null)
                continue;

            JsonSerializerOptions options = new() { Converters = { new EventJsonConverter() } };
            BaseEvent? @event = JsonSerializer.Deserialize<BaseEvent>(consumeResult.Message.Value, options);
            MethodInfo? handlerMethod = this.eventHandler.GetType().GetMethod("On", [@event.GetType()]);

            if(handlerMethod == null)
                throw new ArgumentNullException(nameof(handlerMethod), "Could not find event handler method!");

            handlerMethod.Invoke(this.eventHandler, [@event]);
            consumer.Commit(consumeResult);
        }
    }
}

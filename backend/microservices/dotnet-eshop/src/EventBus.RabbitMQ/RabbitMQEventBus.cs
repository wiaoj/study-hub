using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OpenTelemetry;
using OpenTelemetry.Context.Propagation;
using Polly.Retry;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace EventBus.RabbitMQ;
public sealed class RabbitMQEventBus(
    ILogger<RabbitMQEventBus> logger,
    IServiceProvider serviceProvider,
    IOptions<EventBusOptions> options,
    IOptions<EventBusSubscriptionInfo> subscriptionOptions,
    RabbitMQTelemetry rabbitMQTelemetry) : IEventBus, IDisposable, IHostedService {
    private const String ExchangeName = "eshop_event_bus";

    private readonly ResiliencePipeline pipeline = CreateResiliencePipeline(options.Value.RetryCount);
    private readonly TextMapPropagator propagator = rabbitMQTelemetry.Propagator;
    private readonly ActivitySource activitySource = rabbitMQTelemetry.ActivitySource;
    private readonly String queueName = options.Value.SubscriptionClientName;
    private readonly EventBusSubscriptionInfo subscriptionInfo = subscriptionOptions.Value;
    private IConnection rabbitMQConnection;

    private IModel consumerChannel;

    public Task PublishAsync(IntegrationEvent @event) {
        String routingKey = @event.GetType().Name;

        if(logger.IsEnabled(LogLevel.Trace))
            logger.LogTrace("Creating RabbitMQ channel to publish event: {EventId} ({EventName})", @event.Id, routingKey);

        using IModel channel = this.rabbitMQConnection?.CreateModel() ?? throw new InvalidOperationException("RabbitMQ connection is not open");

        if(logger.IsEnabled(LogLevel.Trace))
            logger.LogTrace("Declaring RabbitMQ exchange to publish event: {EventId}", @event.Id);

        channel.ExchangeDeclare(exchange: ExchangeName, type: "direct");

        Byte[] body = SerializeMessage(@event);

        // Start an activity with a name following the semantic convention of the OpenTelemetry messaging specification.
        // https://github.com/open-telemetry/semantic-conventions/blob/main/docs/messaging/messaging-spans.md
        String activityName = $"{routingKey} publish";

        return this.pipeline.Execute(() => {
            using Activity? activity = this.activitySource.StartActivity(activityName, ActivityKind.Client);

            // Depending on Sampling (and whether a listener is registered or not), the activity above may not be created.
            // If it is created, then propagate its context. If it is not created, the propagate the Current context, if any.

            ActivityContext contextToInject = default;

            if(activity != null)
                contextToInject = activity.Context;
            else if(Activity.Current != null)
                contextToInject = Activity.Current.Context;

            IBasicProperties properties = channel.CreateBasicProperties();
            // persistent
            properties.DeliveryMode = 2;

            static void InjectTraceContextIntoBasicProperties(IBasicProperties props, String key, String value) {
                props.Headers ??= new Dictionary<String, Object>();
                props.Headers[key] = value;
            }

            this.propagator.Inject(new PropagationContext(contextToInject, Baggage.Current), properties, InjectTraceContextIntoBasicProperties);

            SetActivityContext(activity, routingKey, "publish");

            if(logger.IsEnabled(LogLevel.Trace))
                logger.LogTrace("Publishing event to RabbitMQ: {EventId}", @event.Id);

            try {
                channel.BasicPublish(
                    exchange: ExchangeName,
                    routingKey: routingKey,
                    mandatory: true,
                    basicProperties: properties,
                    body: body);

                return Task.CompletedTask;
            }
            catch(Exception ex) {
                activity.SetExceptionTags(ex);

                throw;
            }
        });
    }

    private static void SetActivityContext(Activity activity, String routingKey, String operation) {
        if(activity is not null) {
            // These tags are added demonstrating the semantic conventions of the OpenTelemetry messaging specification
            // https://github.com/open-telemetry/semantic-conventions/blob/main/docs/messaging/messaging-spans.md
            activity.SetTag("messaging.system", "rabbitmq");
            activity.SetTag("messaging.destination_kind", "queue");
            activity.SetTag("messaging.operation", operation);
            activity.SetTag("messaging.destination.name", routingKey);
            activity.SetTag("messaging.rabbitmq.routing_key", routingKey);
        }
    }

    public void Dispose() {
        this.consumerChannel?.Dispose();
    }

    private async Task OnMessageReceived(Object sender, BasicDeliverEventArgs eventArgs) {
        static IEnumerable<String> ExtractTraceContextFromBasicProperties(IBasicProperties props, String key) {
            if(props.Headers.TryGetValue(key, out Object? value)) {
                Byte[]? bytes = value as Byte[];
                return [Encoding.UTF8.GetString(bytes)];
            }
            return [];
        }

        // Extract the PropagationContext of the upstream parent from the message headers.
        PropagationContext parentContext = this.propagator.Extract(default, eventArgs.BasicProperties, ExtractTraceContextFromBasicProperties);
        Baggage.Current = parentContext.Baggage;

        // Start an activity with a name following the semantic convention of the OpenTelemetry messaging specification.
        // https://github.com/open-telemetry/semantic-conventions/blob/main/docs/messaging/messaging-spans.md
        String activityName = $"{eventArgs.RoutingKey} receive";

        using Activity? activity = this.activitySource.StartActivity(activityName, ActivityKind.Client, parentContext.ActivityContext);

        SetActivityContext(activity, eventArgs.RoutingKey, "receive");

        String eventName = eventArgs.RoutingKey;
        String message = Encoding.UTF8.GetString(eventArgs.Body.Span);

        try {
            activity?.SetTag("message", message);

            if(message.Contains("throw-fake-exception", StringComparison.InvariantCultureIgnoreCase))
                throw new InvalidOperationException($"Fake exception requested: \"{message}\"");

            await ProcessEvent(eventName, message);
        }
        catch(Exception ex) {
            logger.LogWarning(ex, "Error Processing message \"{Message}\"", message);

            activity.SetExceptionTags(ex);
        }

        // Even on exception we take the message off the queue.
        // in a REAL WORLD app this should be handled with a Dead Letter Exchange (DLX). 
        // For more information see: https://www.rabbitmq.com/dlx.html
        this.consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
    }

    private async Task ProcessEvent(String eventName, String message) {
        if(logger.IsEnabled(LogLevel.Trace))
            logger.LogTrace("Processing RabbitMQ event: {EventName}", eventName);

        await using AsyncServiceScope scope = serviceProvider.CreateAsyncScope();

        if(!this.subscriptionInfo.EventTypes.TryGetValue(eventName, out Type? eventType)) {
            logger.LogWarning("Unable to resolve event type for event name {EventName}", eventName);
            return;
        }

        // Deserialize the event
        IntegrationEvent integrationEvent = DeserializeMessage(message, eventType);

        // REVIEW: This could be done in parallel

        // Get all the handlers using the event type as the key
        foreach(IIntegrationEventHandler handler in scope.ServiceProvider.GetKeyedServices<IIntegrationEventHandler>(eventType))
            await handler.Handle(integrationEvent);
    }

    [UnconditionalSuppressMessage("Trimming", "IL2026:RequiresUnreferencedCode",
        Justification = "The 'JsonSerializer.IsReflectionEnabledByDefault' feature switch, which is set to false by default for trimmed .NET apps, ensures the JsonSerializer doesn't use Reflection.")]
    [UnconditionalSuppressMessage("AOT", "IL3050:RequiresDynamicCode", Justification = "See above.")]
    private IntegrationEvent DeserializeMessage(String message, Type eventType) {
        return JsonSerializer.Deserialize(message, eventType, this.subscriptionInfo.JsonSerializerOptions) as IntegrationEvent;
    }

    [UnconditionalSuppressMessage("Trimming", "IL2026:RequiresUnreferencedCode",
        Justification = "The 'JsonSerializer.IsReflectionEnabledByDefault' feature switch, which is set to false by default for trimmed .NET apps, ensures the JsonSerializer doesn't use Reflection.")]
    [UnconditionalSuppressMessage("AOT", "IL3050:RequiresDynamicCode", Justification = "See above.")]
    private Byte[] SerializeMessage(IntegrationEvent @event) {
        return JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), this.subscriptionInfo.JsonSerializerOptions);
    }

    public Task StartAsync(CancellationToken cancellationToken) {
        // Messaging is async so we don't need to wait for it to complete. On top of this
        // the APIs are blocking, so we need to run this on a background thread.
        _ = Task.Factory.StartNew(() => {
            try {
                logger.LogInformation("Starting RabbitMQ connection on a background thread");

                this.rabbitMQConnection = serviceProvider.GetRequiredService<IConnection>();
                if(!this.rabbitMQConnection.IsOpen)
                    return;

                if(logger.IsEnabled(LogLevel.Trace))
                    logger.LogTrace("Creating RabbitMQ consumer channel");

                this.consumerChannel = this.rabbitMQConnection.CreateModel();

                this.consumerChannel.CallbackException += (sender, ea) => {
                    logger.LogWarning(ea.Exception, "Error with RabbitMQ consumer channel");
                };

                this.consumerChannel.ExchangeDeclare(exchange: ExchangeName,
                                        type: "direct");

                this.consumerChannel.QueueDeclare(queue: this.queueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                if(logger.IsEnabled(LogLevel.Trace))
                    logger.LogTrace("Starting RabbitMQ basic consume");

                AsyncEventingBasicConsumer consumer = new(this.consumerChannel);

                consumer.Received += OnMessageReceived;

                this.consumerChannel.BasicConsume(
                    queue: this.queueName,
                    autoAck: false,
                    consumer: consumer);

                foreach((String eventName, Type _) in this.subscriptionInfo.EventTypes)
                    this.consumerChannel.QueueBind(
                        queue: this.queueName,
                        exchange: ExchangeName,
                        routingKey: eventName);
            }
            catch(Exception ex) {
                logger.LogError(ex, "Error starting RabbitMQ connection");
            }
        },
        TaskCreationOptions.LongRunning);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) {
        return Task.CompletedTask;
    }

    private static ResiliencePipeline CreateResiliencePipeline(Int32 retryCount) {
        // See https://www.pollydocs.org/strategies/retry.html
        RetryStrategyOptions retryOptions = new() {
            ShouldHandle = new PredicateBuilder().Handle<BrokerUnreachableException>().Handle<SocketException>(),
            MaxRetryAttempts = retryCount,
            DelayGenerator = (context) => ValueTask.FromResult(GenerateDelay(context.AttemptNumber))
        };

        return new ResiliencePipelineBuilder()
            .AddRetry(retryOptions)
            .Build();

        static TimeSpan? GenerateDelay(Int32 attempt) => TimeSpan.FromSeconds(Math.Pow(2, attempt));
    }
}
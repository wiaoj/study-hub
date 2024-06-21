namespace EventBus.RabbitMQ;
public class EventBusOptions {
    public String SubscriptionClientName { get; set; }
    public Int32 RetryCount { get; set; } = 10;
}
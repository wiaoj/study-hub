namespace MassTransit.EventSourcing.Command.Events;
public sealed record PostCreatedEvent() : BaseEvent(nameof(PostCreatedEvent)) {
    public String Author { get; set; }
    public String Text { get; set; }
    public DateTime DatePosted { get; set; }
}
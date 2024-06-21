namespace EventBus.Events;
public record IntegrationEvent {
    [JsonInclude]
    public Guid Id { get; private set; }

    [JsonInclude]
    public DateTime CreationDate { get; private set; }

    public IntegrationEvent() {
        this.Id = Guid.NewGuid();
        this.CreationDate = DateTime.UtcNow;
    }
}
using System.ComponentModel.DataAnnotations;

namespace IntegrationEventLogEF;
public class IntegrationEventLogEntry {
    private static readonly JsonSerializerOptions s_indentedOptions = new() { WriteIndented = true };
    private static readonly JsonSerializerOptions s_caseInsensitiveOptions = new() { PropertyNameCaseInsensitive = true };

    public Guid EventId { get; private set; }
    [Required]
    public String EventTypeName { get; private set; }
    [NotMapped]
    public String EventTypeShortName => this.EventTypeName.Split('.')?.Last();
    [NotMapped]
    public IntegrationEvent IntegrationEvent { get; private set; }
    public EventState State { get; set; }
    public Int32 TimesSent { get; set; }
    public DateTime CreationTime { get; private set; }
    [Required]
    public String Content { get; private set; }
    public Guid TransactionId { get; private set; }


    private IntegrationEventLogEntry() { }
    public IntegrationEventLogEntry(IntegrationEvent @event, Guid transactionId) {
        this.EventId = @event.Id;
        this.CreationTime = @event.CreationDate;
        this.EventTypeName = @event.GetType().FullName;
        this.Content = JsonSerializer.Serialize(@event, @event.GetType(), s_indentedOptions);
        this.State = EventState.NotPublished;
        this.TimesSent = 0;
        this.TransactionId = transactionId;
    }

    public IntegrationEventLogEntry DeserializeJsonContent(Type type) {
        this.IntegrationEvent = JsonSerializer.Deserialize(this.Content, type, s_caseInsensitiveOptions) as IntegrationEvent;
        return this;
    }
}
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MassTransit.EventSourcing.Command.Events;
public class EventModel {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public String Id { get; set; }
    public DateTime TimeStamp { get; set; }
    public String AggregateIdentifier { get; set; }
    public String AggregateType { get; set; }
    public Int32 Version { get; set; }
    public String EventType { get; set; }
    public BaseEvent EventData { get; set; }

    public static EventModel FromBaseEvent(BaseEvent baseEvent) {
        return new EventModel { 
            TimeStamp = DateTime.UtcNow,
            AggregateIdentifier = baseEvent.AggregateIdentifier,
            AggregateType = baseEvent.AggregateType,
            Version = baseEvent.Version,
            EventType = baseEvent.Type,
            EventData = baseEvent
        };
    }
}

public abstract record BaseEvent {
    protected BaseEvent(String type) {
        this.Type = type;
    }

    public required String Id { get; set; }
    public required String AggregateIdentifier { get; set; }
    public required String AggregateType { get; set; }
    public String Type { get; }
    public required Int32 Version { get; set; }
}
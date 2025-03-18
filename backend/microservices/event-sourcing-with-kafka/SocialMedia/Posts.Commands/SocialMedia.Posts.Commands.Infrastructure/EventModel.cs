using CQRS.EventSourcing.Core.Domain;
using CQRS.EventSourcing.Core.Events;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SocialMedia.Posts.Commands.Infrastructure;
public class EventModel {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public String Id { get; set; }
    public DateTime TimeStamp { get; set; }
    public Guid AggregateIdentifier { get; set; }
    public String AggregateType { get; set; }
    public Int32 Version { get; set; }
    public String EventType { get; set; }
    public BaseEvent EventData { get; set; }
}
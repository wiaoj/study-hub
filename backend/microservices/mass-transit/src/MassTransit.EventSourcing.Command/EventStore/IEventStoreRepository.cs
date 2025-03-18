using MassTransit.EventSourcing.Command.Events;
using MongoDB.Driver;

namespace MassTransit.EventSourcing.Command.EventStore;
public interface IEventStoreRepository {
    Task SaveAsync<T>(IEnumerable<T> events) where T : BaseEvent;
    Task<IEnumerable<T>> GetEventsAsync<T>(String aggregateId) where T : BaseEvent;
}
public class MongoDbEventStoreRepository : IEventStoreRepository {
    private readonly IMongoCollection<EventModel> eventStoreCollection;

    public MongoDbEventStoreRepository() {
        MongoClient mongoClient = new("mongodb+srv://wiaoj:FsqdJVlbEWgaYvfW@cluster0.kk9im84.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0");
        IMongoDatabase mongoDatabase = mongoClient.GetDatabase("event_data");

        this.eventStoreCollection = mongoDatabase.GetCollection<EventModel>("events");
    }

    public async Task<IEnumerable<T>> GetEventsAsync<T>(String aggregateId) where T : BaseEvent {
        FilterDefinition<EventModel> filter = Builders<EventModel>.Filter.Eq(x => x.AggregateIdentifier, aggregateId);
        IAsyncCursor<EventModel> cursor = await this.eventStoreCollection.FindAsync(filter);
        IEnumerable<T> events = cursor.ToList().Select(x => x.EventData).OfType<T>();
        return events;
    }

    public async Task SaveAsync<T>(IEnumerable<T> events) where T : BaseEvent {
        IEnumerable<EventModel> eventModels = events.Select(EventModel.FromBaseEvent);
        await this.eventStoreCollection.InsertManyAsync(eventModels, null, default);
    }
}

public class InMemoryEventStoreRepository : IEventStoreRepository {
    private readonly List<EventModel> events = [];

    public async Task SaveAsync<T>(IEnumerable<T> events) where T : BaseEvent {
        IEnumerable<EventModel> eventModels = events.Select(EventModel.FromBaseEvent);
        this.events.AddRange(eventModels);
        await Task.CompletedTask;
    }

    public async Task<IEnumerable<T>> GetEventsAsync<T>(String aggregateId) where T : BaseEvent {
        IEnumerable<T> events = this.events
            .Where(e => e.AggregateIdentifier == aggregateId)
            .Select(e => e.EventData)
            .Cast<T>();

        return await Task.FromResult(events);
    }
}
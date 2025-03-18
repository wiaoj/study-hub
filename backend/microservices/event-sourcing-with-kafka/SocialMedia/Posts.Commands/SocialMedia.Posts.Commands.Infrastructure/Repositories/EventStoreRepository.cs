using CQRS.EventSourcing.Core.Domain;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SocialMedia.Posts.Commands.Infrastructure.Config;

namespace SocialMedia.Posts.Commands.Infrastructure.Repositories;
public class EventStoreRepository : IEventStoreRepository {
    private readonly IMongoCollection<EventModel> eventStoreCollection;

    public EventStoreRepository(IOptions<MongoDbConfig> config) {
        MongoClient mongoClient = new(config.Value.ConnectionString);
        IMongoDatabase mongoDatabase = mongoClient.GetDatabase(config.Value.Database);

        this.eventStoreCollection = mongoDatabase.GetCollection<EventModel>(config.Value.Collection);
    }

    public async Task<List<EventModel>> FindAllAsync(CancellationToken cancellationToken) {
        return await this.eventStoreCollection.Find(_ => true)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task<List<EventModel>> FindByAggregateId<TId>(TId aggregateId, CancellationToken cancellationToken)
        where TId : AggregateRootId {
        return await this.eventStoreCollection
            .Find(x => x.AggregateIdentifier == aggregateId.Value)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    public async Task SaveAsync(EventModel @event, CancellationToken cancellationToken) {
        await this.eventStoreCollection
            .InsertOneAsync(@event, null, cancellationToken)
            .ConfigureAwait(false);
    }
}
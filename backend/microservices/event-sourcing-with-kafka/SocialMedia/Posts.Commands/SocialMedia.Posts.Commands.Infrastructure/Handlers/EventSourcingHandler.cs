using CQRS.EventSourcing.Core.Domain;
using CQRS.EventSourcing.Core.Events;
using CQRS.EventSourcing.Core.Handlers;
using CQRS.EventSourcing.Core.Infrastructure;
using CQRS.EventSourcing.Core.Producers;
using Microsoft.Extensions.Options;
using SocialMedia.Posts.Commands.Domain.Aggregates;
using SocialMedia.Posts.Commands.Infrastructure.Config;
using SocialMedia.Posts.Common.Extensions;

namespace SocialMedia.Posts.Commands.Infrastructure.Handlers;
public class EventSourcingHandler(IEventStore eventStore, IEventProducer eventProducer, IOptions<MessageQueueConfig> options) : IEventSourcingHandler<PostAggregate, PostId> {
    public async Task<PostAggregate> GetByIdAsync(PostId aggregateId, CancellationToken cancellationToken) {
        PostAggregate aggregate = new();
        List<BaseEvent> events = await eventStore.GetEventsAsync(aggregateId, cancellationToken);

        if(events.IsNull() || events.IsZero())
            return aggregate;

        aggregate.ReplayEvents(events);
        aggregate.Version = events.Max(x => x.Version);

        return aggregate;
    }

    public async Task RepublishEventsAsync(CancellationToken cancellationToken) {
        List<PostId> aggregateIds = (await eventStore.GetAggregateIdsAsync<PostId>(cancellationToken)).ConvertAll(x => new PostId(x));

        if(aggregateIds.IsNull() || aggregateIds.IsZero())
            return;

        foreach(PostId aggregateId in aggregateIds) {
            PostAggregate aggregate = await GetByIdAsync(aggregateId, cancellationToken);

            if(aggregate.IsNull() || aggregate.IsActive.IsFalse())
                continue;

            List<BaseEvent> events = await eventStore.GetEventsAsync(aggregateId, cancellationToken);

            foreach(BaseEvent @event in events)
                await eventProducer.ProduceAsync(options.Value.Topic, @event);
        }
    }

    public async Task SaveAsync(AggregateRoot<PostId> aggregate, CancellationToken cancellationToken) {
        await eventStore.SaveEventsAsync(aggregate.Id, aggregate.GetUncommittedChanges(), aggregate.Version, cancellationToken);
        aggregate.MarkChangesAsCommitted();
    }
}
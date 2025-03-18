using CQRS.EventSourcing.Core.Domain;
using CQRS.EventSourcing.Core.Events;

namespace CQRS.EventSourcing.Core.Infrastructure;
public interface IEventStore {  
    Task<List<Guid>> GetAggregateIdsAsync<TId>(CancellationToken cancellationToken) where TId : AggregateRootId;
    Task<List<BaseEvent>> GetEventsAsync<TId>(TId aggregateId, CancellationToken cancellationToken) where TId : AggregateRootId;
    Task SaveEventsAsync<TId>(TId aggregateId,
                              IEnumerable<BaseEvent> events,
                              Int32 expectedVersion,
                              CancellationToken cancellationToken) where TId : AggregateRootId;
}
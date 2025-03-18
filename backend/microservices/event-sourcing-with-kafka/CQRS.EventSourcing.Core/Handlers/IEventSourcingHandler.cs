using CQRS.EventSourcing.Core.Domain;

namespace CQRS.EventSourcing.Core.Handlers;
public interface IEventSourcingHandler<TAggregate, TId> where TId : AggregateRootId {
    Task SaveAsync(AggregateRoot<TId> aggregate, CancellationToken cancellationToken);
    Task<TAggregate> GetByIdAsync(TId aggregateId, CancellationToken cancellationToken);
    Task RepublishEventsAsync(CancellationToken cancellationToken);
}
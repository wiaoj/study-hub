namespace CQRS.EventSourcing.Core.Domain;
public abstract record AggregateRootId(Guid Value) : EntityId(Value);
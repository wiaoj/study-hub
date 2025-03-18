using CQRS.EventSourcing.Core.Events;
using System.Reflection;

namespace CQRS.EventSourcing.Core.Domain;
public abstract class AggregateRoot<TId> where TId : AggregateRootId {
    public TId Id { get; private set; }
    private readonly List<BaseEvent> changes = [];
    public Int32 Version { get; set; } = -1;

    public AggregateRoot() { }
    protected AggregateRoot(TId id) {
        this.Id = id;
    }

    public IEnumerable<BaseEvent> GetUncommittedChanges() {
        return this.changes;
    }

    public void MarkChangesAsCommitted() {
        this.changes.Clear();
    }

    private void ApplyChange(BaseEvent @event, Boolean isNew) {
        MethodInfo? method = GetType().GetMethod("Apply", [@event.GetType()]);

        if(method == null)
            throw new ArgumentNullException(nameof(method), $"The Apply method was not found in the aggregate for {@event.GetType().Name}!");

        method.Invoke(this, [@event]);

        if(isNew)
            this.changes.Add(@event);
    }

    //TODO
    public void Apply<TEvent>(TEvent @event) where TEvent : BaseEvent {

    }

    protected void RaiseEvent(BaseEvent @event) {
        ApplyChange(@event, true);
    }

    public void ReplayEvents(IEnumerable<BaseEvent> events) {
        foreach(BaseEvent @event in events)
            ApplyChange(@event, false);
    }
}
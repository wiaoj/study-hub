namespace CQRS.EventSourcing.Core.Domain;
public abstract record EntityId {
    public Guid Value { get; private set; }

    protected EntityId(Guid id) {
        this.Value = id;
    }
}
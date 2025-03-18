namespace CQRS.EventSourcing.Core.Events;
public abstract record BaseEvent {
    public String Type => GetType().Name;
    public Int32 Version { get; private set; }

    public BaseEvent IncreaseVersion() {
        this.Version++;
        return this;
    }
}
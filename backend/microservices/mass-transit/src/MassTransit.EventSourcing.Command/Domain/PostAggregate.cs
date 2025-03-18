using MassTransit.EventSourcing.Command.Events;

namespace MassTransit.EventSourcing.Command.Domain;
public partial class PostAggregate {
    private List<BaseEvent> events;
    public String Id { get; private set; }
    public String Text { get; private set; }
    public Boolean Active { get; private set; }
    public Int64 Version { get; private set; }
    public List<CommentEntity> Comments { get; private set; }

    public IReadOnlyCollection<BaseEvent> Events => this.events.AsReadOnly();

    private PostAggregate() {
        this.events = [];
    }

    private PostAggregate(String id, String text, List<CommentEntity> comments) {
        this.events = [];
        this.Id = id;
        this.Text = text;
        this.Comments = comments;
        this.Active = true;
    }

    public static PostAggregate Empty => new();

    public static PostAggregate Create(String text) {
        return new(Ulid.NewUlid().ToString(), text, []);
    }

    public void RaiseEvent(BaseEvent @event) {
        this.events.Add(@event);
    }

    internal void IncreaseVersion() {
        this.Version = this.Version++;
    }
}

public sealed class CommentEntity {
    public String Id { get; private set; }
    public String Text { get; private set; }

    private CommentEntity(String id, String text) {
        this.Id = id;
        this.Text = text;
    }

    public static CommentEntity Create(String text) {
        return new(Ulid.NewUlid().ToString(), text);
    }
}
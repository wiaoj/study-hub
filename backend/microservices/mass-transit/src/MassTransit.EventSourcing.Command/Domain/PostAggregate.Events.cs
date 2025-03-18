using MassTransit.EventSourcing.Command.Events;
using System.Security.Cryptography;

namespace MassTransit.EventSourcing.Command.Domain;
public partial class PostAggregate { 
    public void Apply(BaseEvent @event) {
        switch(@event) {
            case PostCreatedEvent postCreatedEvent:
                Apply(postCreatedEvent);
                break;
            default:
            break;
        }
    }

    private void Apply(PostCreatedEvent @event) {
        this.Id = @event.Id;
        this.Text = @event.Text;
        this.Active = true;
        this.IncreaseVersion();
    }
}
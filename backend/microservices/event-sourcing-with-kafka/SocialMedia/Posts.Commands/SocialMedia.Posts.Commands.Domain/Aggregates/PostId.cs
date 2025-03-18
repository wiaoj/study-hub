using CQRS.EventSourcing.Core.Domain;

namespace SocialMedia.Posts.Commands.Domain.Aggregates;
public sealed record PostId(Guid Value) : AggregateRootId(Value) {
    public static PostId CreateNew() {
        return new(Guid.NewGuid());
    }
}

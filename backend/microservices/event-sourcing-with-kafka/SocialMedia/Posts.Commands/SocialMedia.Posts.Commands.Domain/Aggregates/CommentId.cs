using CQRS.EventSourcing.Core.Domain;

namespace SocialMedia.Posts.Commands.Domain.Aggregates;
public sealed record CommentId(Guid Value) : EntityId(Value) {
    public static CommentId CreateNew() {
        return new(Guid.NewGuid());
    }
}
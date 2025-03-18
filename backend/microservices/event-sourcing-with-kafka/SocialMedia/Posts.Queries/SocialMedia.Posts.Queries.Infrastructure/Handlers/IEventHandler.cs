using SocialMedia.Posts.Common.Events;

namespace SocialMedia.Posts.Queries.Infrastructure.Handlers;
public interface IEventHandler {
    Task On(PostCreatedEvent @event);
    Task On(MessageUpdatedEvent @event);
    Task On(PostLikedEvent @event);
    Task On(PostCommentCreatedEvent @event);
    Task On(CommentUpdatedEvent @event);
    Task On(CommentRemovedEvent @event);
    Task On(PostRemovedEvent @event);
}
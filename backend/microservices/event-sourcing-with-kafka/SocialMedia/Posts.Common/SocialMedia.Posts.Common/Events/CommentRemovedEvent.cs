using CQRS.EventSourcing.Core.Events;

namespace SocialMedia.Posts.Common.Events;
public sealed record CommentRemovedEvent(Guid PostId, Guid CommentId) : BaseEvent;
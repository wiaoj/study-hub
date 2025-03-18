using CQRS.EventSourcing.Core.Events;

namespace SocialMedia.Posts.Common.Events;
public sealed record PostCommentCreatedEvent(Guid PostId,
                                             Guid CommentId,
                                             String Comment,
                                             String Username,
                                             DateTime CommentCreatedAt) : BaseEvent;
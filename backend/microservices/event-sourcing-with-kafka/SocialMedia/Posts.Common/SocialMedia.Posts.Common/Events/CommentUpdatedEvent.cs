using CQRS.EventSourcing.Core.Events;

namespace SocialMedia.Posts.Common.Events;
public sealed record CommentUpdatedEvent(Guid PostId,
                                         Guid CommentId,
                                         String Comment,
                                         String Username,
                                         DateTime EditDate) : BaseEvent;
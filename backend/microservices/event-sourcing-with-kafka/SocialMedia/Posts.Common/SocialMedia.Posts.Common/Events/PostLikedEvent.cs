using CQRS.EventSourcing.Core.Events;

namespace SocialMedia.Posts.Common.Events;
public sealed record PostLikedEvent(Guid PostId) : BaseEvent;
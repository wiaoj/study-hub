using CQRS.EventSourcing.Core.Events;

namespace SocialMedia.Posts.Common.Events;
public sealed record MessageUpdatedEvent(Guid PostId, String Message) : BaseEvent;
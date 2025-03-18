using CQRS.EventSourcing.Core.Events;

namespace SocialMedia.Posts.Common.Events;
public sealed record PostCreatedEvent(Guid PostId,
                                      String Author,
                                      String Message,
                                      DateTime DatePosted) : BaseEvent;
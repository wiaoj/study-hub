using CQRS.EventSourcing.Core.Abstractions;

namespace SocialMedia.Posts.Commands.Api.Features.DeletePost;
public sealed record DeletePostCommand(Guid PostId, String Username) : IBaseCommand;
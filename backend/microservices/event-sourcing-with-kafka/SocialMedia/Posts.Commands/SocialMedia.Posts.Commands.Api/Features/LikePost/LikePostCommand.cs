using CQRS.EventSourcing.Core.Abstractions;

namespace SocialMedia.Posts.Commands.Api.Features.LikePost;
public sealed record LikePostCommand(Guid Id) : IBaseCommand;
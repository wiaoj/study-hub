using CQRS.EventSourcing.Core.Abstractions;

namespace SocialMedia.Posts.Commands.Api.Features.AddComment;
public sealed record AddCommentCommand(String Comment, String Username) : IBaseCommand {
    public Guid? PostId { get; set; }
}
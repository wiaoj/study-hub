using CQRS.EventSourcing.Core.Abstractions;

namespace SocialMedia.Posts.Commands.Api.Features.RemoveComment;
public class RemoveCommentCommand : IBaseCommand {
    public Guid PostId { get; set; }
    public Guid CommentId { get; set; }
    public String Username { get; set; }
}
using CQRS.EventSourcing.Core.Abstractions;

namespace SocialMedia.Posts.Commands.Api.Features.EditComment;
public class EditCommentCommand : IBaseCommand {
    public Guid PostId { get; set; }
    public Guid CommentId { get; set; }
    public String Comment { get; set; }
    public String Username { get; set; }
}
using CQRS.EventSourcing.Core.Abstractions;

namespace SocialMedia.Posts.Commands.Api.Features.NewPost;
public class NewPostCommand : IBaseCommand {
    public String Author { get; set; }
    public String Message { get; set; }
}
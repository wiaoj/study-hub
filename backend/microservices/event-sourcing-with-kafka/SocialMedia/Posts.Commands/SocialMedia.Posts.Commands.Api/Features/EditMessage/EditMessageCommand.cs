using CQRS.EventSourcing.Core.Abstractions;

namespace SocialMedia.Posts.Commands.Api.Features.EditMessage;
public class EditMessageCommand : IBaseCommand {
    public Guid PostId { get; set; }
    public String Message { get; set; }
}
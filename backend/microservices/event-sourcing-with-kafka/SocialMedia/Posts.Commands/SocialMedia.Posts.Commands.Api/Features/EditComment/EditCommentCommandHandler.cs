using CQRS.EventSourcing.Core.Abstractions;
using CQRS.EventSourcing.Core.Handlers;
using SocialMedia.Posts.Commands.Domain.Aggregates;

namespace SocialMedia.Posts.Commands.Api.Features.EditComment;
internal sealed class EditCommentCommandHandler(IEventSourcingHandler<PostAggregate, PostId> eventSourcingHandler)
    : ICommandHandler<EditCommentCommand> {
    public async Task HandleAsync(EditCommentCommand command, CancellationToken cancellationToken) {
        PostId postId = new(command.PostId);
        PostAggregate aggregate = await eventSourcingHandler.GetByIdAsync(postId, cancellationToken);


        CommentEntity comment = new(new CommentId(command.CommentId), command.Username, command.Comment);
        aggregate.EditComment(comment);

        await eventSourcingHandler.SaveAsync(aggregate, cancellationToken);
    }
}
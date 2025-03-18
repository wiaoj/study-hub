using CQRS.EventSourcing.Core.Abstractions;
using CQRS.EventSourcing.Core.Handlers;
using SocialMedia.Posts.Commands.Domain.Aggregates;

namespace SocialMedia.Posts.Commands.Api.Features.RemoveComment;
internal sealed class RemoveCommentCommandHandler(IEventSourcingHandler<PostAggregate, PostId> eventSourcingHandler)
    : ICommandHandler<RemoveCommentCommand> {
    public async Task HandleAsync(RemoveCommentCommand command, CancellationToken cancellationToken) {
        PostId postId = new(command.PostId);
        PostAggregate aggregate = await eventSourcingHandler.GetByIdAsync(postId, cancellationToken);

        CommentId commentId = new(command.CommentId);
        aggregate.RemoveComment(commentId, command.Username);

        await eventSourcingHandler.SaveAsync(aggregate, cancellationToken);
    }
}
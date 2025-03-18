using CQRS.EventSourcing.Core.Abstractions;
using CQRS.EventSourcing.Core.Handlers;
using SocialMedia.Posts.Commands.Domain.Aggregates;

namespace SocialMedia.Posts.Commands.Api.Features.AddComment;
internal sealed class AddCommentCommandHandler(IEventSourcingHandler<PostAggregate, PostId> eventSourcingHandler)
    : ICommandHandler<AddCommentCommand> {
    public async Task HandleAsync(AddCommentCommand command, CancellationToken cancellationToken) {
        PostId postId = new(Guid.Parse(command.PostId.ToString()!));
        PostAggregate aggregate = await eventSourcingHandler.GetByIdAsync(postId, cancellationToken);

        CommentEntity comment = CommentEntity.CreateNew(command.Comment, command.Username);
        aggregate.AddComment(comment);

        await eventSourcingHandler.SaveAsync(aggregate, cancellationToken);
    }
}
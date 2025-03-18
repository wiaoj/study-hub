using CQRS.EventSourcing.Core.Abstractions;
using CQRS.EventSourcing.Core.Handlers;
using SocialMedia.Posts.Commands.Domain.Aggregates;

namespace SocialMedia.Posts.Commands.Api.Features.DeletePost;
internal sealed class DeletePostCommandHandler(IEventSourcingHandler<PostAggregate, PostId> eventSourcingHandler)
    : ICommandHandler<DeletePostCommand> {
    public async Task HandleAsync(DeletePostCommand command, CancellationToken cancellationToken) {
        PostId postId = new(command.PostId);
        PostAggregate aggregate = await eventSourcingHandler.GetByIdAsync(postId, cancellationToken);
        aggregate.DeletePost(command.Username);

        await eventSourcingHandler.SaveAsync(aggregate, cancellationToken);
    }
}
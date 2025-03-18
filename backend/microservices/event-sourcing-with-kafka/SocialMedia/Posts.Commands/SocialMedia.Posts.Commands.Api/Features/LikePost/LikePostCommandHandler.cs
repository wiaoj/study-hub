using CQRS.EventSourcing.Core.Abstractions;
using CQRS.EventSourcing.Core.Handlers;
using SocialMedia.Posts.Commands.Domain.Aggregates;

namespace SocialMedia.Posts.Commands.Api.Features.LikePost;
internal sealed class LikePostCommandHandler(IEventSourcingHandler<PostAggregate, PostId> eventSourcingHandler)
    : ICommandHandler<LikePostCommand> {
    public async Task HandleAsync(LikePostCommand command, CancellationToken cancellationToken) {
        PostId postId = new(command.Id);
        PostAggregate aggregate = await eventSourcingHandler.GetByIdAsync(postId, cancellationToken);
        aggregate.LikePost();

        await eventSourcingHandler.SaveAsync(aggregate, cancellationToken);
    }
}
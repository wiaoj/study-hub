using CQRS.EventSourcing.Core.Abstractions;
using CQRS.EventSourcing.Core.Handlers;
using SocialMedia.Posts.Commands.Domain.Aggregates;

namespace SocialMedia.Posts.Commands.Api.Features.EditMessage;
internal sealed class EditMessageCommandHandler(IEventSourcingHandler<PostAggregate, PostId> eventSourcingHandler)
    : ICommandHandler<EditMessageCommand> {
    public async Task HandleAsync(EditMessageCommand command, CancellationToken cancellationToken) {
        PostId postId = new(command.PostId);
        PostAggregate aggregate = await eventSourcingHandler.GetByIdAsync(postId, cancellationToken);
        aggregate.EditMessage(command.Message);

        await eventSourcingHandler.SaveAsync(aggregate, cancellationToken);
    }
}
using CQRS.EventSourcing.Core.Abstractions;
using CQRS.EventSourcing.Core.Handlers;
using SocialMedia.Posts.Commands.Domain.Aggregates;

namespace SocialMedia.Posts.Commands.Api.Features.NewPost;
internal sealed class NewPostCommandHandler(IEventSourcingHandler<PostAggregate, PostId> eventSourcingHandler)
    : ICommandHandler<NewPostCommand> {
    public async Task HandleAsync(NewPostCommand command, CancellationToken cancellationToken) {
        PostAggregate aggregate = PostAggregate.CreateNew(command.Author, command.Message);

        await eventSourcingHandler.SaveAsync(aggregate, cancellationToken);
    }
}
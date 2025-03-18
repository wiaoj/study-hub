using CQRS.EventSourcing.Core.Abstractions;
using CQRS.EventSourcing.Core.Handlers;
using SocialMedia.Posts.Commands.Domain.Aggregates;

namespace SocialMedia.Posts.Commands.Api.Features.RestoreReadDb;
internal sealed class RestoreReadDbCommandHandler(IEventSourcingHandler<PostAggregate, PostId> eventSourcingHandler)
    : ICommandHandler<RestoreReadDbCommand> {
    public async Task HandleAsync(RestoreReadDbCommand command, CancellationToken cancellationToken) {
        await eventSourcingHandler.RepublishEventsAsync(cancellationToken);
    }
}
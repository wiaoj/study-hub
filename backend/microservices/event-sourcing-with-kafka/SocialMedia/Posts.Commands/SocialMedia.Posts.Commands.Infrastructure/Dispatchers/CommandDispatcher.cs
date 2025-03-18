using CQRS.EventSourcing.Core.Abstractions;
using CQRS.EventSourcing.Core.Infrastructure;

namespace SocialMedia.Posts.Commands.Infrastructure.Dispatchers;
public class CommandDispatcher<TCommand> : ICommandDispatcher<TCommand> where TCommand : IBaseCommand {
    private readonly ICommandHandler<TCommand> handler;

    public CommandDispatcher(ICommandHandler<TCommand> handler) {
        this.handler = handler;
    }

    public async Task SendAsync(TCommand command, CancellationToken cancellationToken) {
        await this.handler.HandleAsync(command, cancellationToken);
    } 
} 
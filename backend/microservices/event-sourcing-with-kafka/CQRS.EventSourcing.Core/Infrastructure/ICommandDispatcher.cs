using CQRS.EventSourcing.Core.Abstractions;

namespace CQRS.EventSourcing.Core.Infrastructure;
public interface ICommandDispatcher {
    Task SendAsync(IBaseCommand command, CancellationToken cancellationToken = default);
}
public interface ICommandDispatcher<in TCommand> where TCommand : IBaseCommand {
    Task SendAsync(TCommand command, CancellationToken cancellationToken);
}
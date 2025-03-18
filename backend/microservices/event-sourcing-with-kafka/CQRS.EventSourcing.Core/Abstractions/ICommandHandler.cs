namespace CQRS.EventSourcing.Core.Abstractions {
    public interface ICommandHandler<in ICommand> where ICommand : IBaseCommand {
        Task HandleAsync(ICommand command, CancellationToken cancellationToken);
    }
}
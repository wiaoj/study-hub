using CQRS.EventSourcing.Core.Abstractions;

namespace CQRS.EventSourcing.Core.Infrastructure;
public interface IQueryDispatcher<TEntity> {
    void RegisterHandler<TQuery>(Func<TQuery, Task<List<TEntity>>> handler) where TQuery : IBaseQuery;
    Task<List<TEntity>> SendAsync(IBaseQuery query);
}
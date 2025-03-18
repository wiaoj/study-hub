using CQRS.EventSourcing.Core.Abstractions;
using CQRS.EventSourcing.Core.Exceptions;
using CQRS.EventSourcing.Core.Infrastructure;
using SocialMedia.Posts.Queries.Domain.Entities;

namespace SocialMedia.Posts.Queries.Infrastructure.Dispatchers;
public sealed class PostQueryDispatcher : IQueryDispatcher<PostEntity> {
    private readonly Dictionary<Type, Func<IBaseQuery, Task<List<PostEntity>>>> handlers = [];

    public void RegisterHandler<TQuery>(Func<TQuery, Task<List<PostEntity>>> handler) where TQuery : IBaseQuery {
        Type queryType = typeof(TQuery);
        if(this.handlers.ContainsKey(queryType))
            throw new DuplicateHandlerException();

        this.handlers.Add(queryType, x => handler((TQuery)x));
    }

    public async Task<List<PostEntity>> SendAsync(IBaseQuery query) {
        if(this.handlers.TryGetValue(query.GetType(), out Func<IBaseQuery, Task<List<PostEntity>>>? handler) is false)
            throw new NoHandlerRegisteredException(nameof(handler));
        return await handler(query);
    }
}
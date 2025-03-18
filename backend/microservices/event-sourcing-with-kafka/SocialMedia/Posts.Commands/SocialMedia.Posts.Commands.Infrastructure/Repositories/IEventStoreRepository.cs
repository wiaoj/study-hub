using SocialMedia.Posts.Commands.Infrastructure;
using System.Threading;

namespace CQRS.EventSourcing.Core.Domain;
public interface IEventStoreRepository {
    Task SaveAsync(EventModel @event, CancellationToken cancellationToken);
    Task<List<EventModel>> FindByAggregateId<TId>(TId aggregateId, CancellationToken cancellationToken) where TId : AggregateRootId;
    Task<List<EventModel>> FindAllAsync(CancellationToken cancellationToken); 
}
using CQRS.EventSourcing.Core.Abstractions;

namespace SocialMedia.Posts.Queries.Api.Queries;
public class FindPostByIdQuery : IBaseQuery {
    public Guid Id { get; set; }
}
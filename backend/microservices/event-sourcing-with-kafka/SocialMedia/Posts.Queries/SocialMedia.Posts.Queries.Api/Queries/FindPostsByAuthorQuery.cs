using CQRS.EventSourcing.Core.Abstractions;

namespace SocialMedia.Posts.Queries.Api.Queries;
public class FindPostsByAuthorQuery : IBaseQuery {
    public String Author { get; set; }
}
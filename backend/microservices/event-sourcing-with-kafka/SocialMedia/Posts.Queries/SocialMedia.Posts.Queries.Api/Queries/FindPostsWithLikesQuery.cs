using CQRS.EventSourcing.Core.Abstractions;

namespace SocialMedia.Posts.Queries.Api.Queries;
public class FindPostsWithLikesQuery : IBaseQuery {
    public Int32 NumberOfLikes { get; set; }
}
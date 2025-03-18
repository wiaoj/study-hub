using SocialMedia.Posts.Queries.Domain.Entities;

namespace SocialMedia.Posts.Queries.Domain.Repositories;
public interface IPostRepository {
    Task CreateAsync(PostEntity post);
    Task UpdateAsync(PostEntity post);
    Task DeleteAsync(Guid postId);
    Task<PostEntity> GetByIdAsync(Guid postId);
    Task<List<PostEntity>> ListAllAsync();
    Task<List<PostEntity>> ListByAuthorAsync(String author);
    Task<List<PostEntity>> ListWithLikesAsync(Int32 numberOfLikes);
    Task<List<PostEntity>> ListWithCommentsAsync();
}
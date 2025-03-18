using SocialMedia.Posts.Queries.Domain.Entities;

namespace SocialMedia.Posts.Queries.Domain.Repositories;
public interface ICommentRepository {
    Task CreateAsync(CommentEntity comment);
    Task<CommentEntity> GetByIdAsync(Guid commentId);
    Task UpdateAsync(CommentEntity comment);
    Task DeleteAsync(Guid commentId);
}
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SocialMedia.Posts.Queries.Domain.Entities;
using SocialMedia.Posts.Queries.Domain.Repositories;
using SocialMedia.Posts.Queries.Infrastructure.DataAccess;

namespace Post.Query.Infrastructure.Repositories {
    public class PostRepository : IPostRepository
    {
        private readonly DatabaseContextFactory _contextFactory;

        public PostRepository(DatabaseContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task CreateAsync(PostEntity post)
        {
            using ApplicationDatabaseContext context = _contextFactory.CreateDbContext();
            context.Posts.Add(post);

            _ = await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid postId)
        {
            using ApplicationDatabaseContext context = _contextFactory.CreateDbContext();
            var post = await GetByIdAsync(postId);

            if (post == null) return;

            context.Posts.Remove(post);
            _ = await context.SaveChangesAsync();
        }

        public async Task<List<PostEntity>> ListByAuthorAsync(string author)
        {
            using ApplicationDatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Posts.AsNoTracking()
                    .Include(i => i.Comments).AsNoTracking()
                    .Where(x => x.Author.Contains(author))
                    .ToListAsync();
        }

        public async Task<PostEntity> GetByIdAsync(Guid postId)
        {
            using ApplicationDatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Posts
                    .Include(i => i.Comments)
                    .FirstOrDefaultAsync(x => x.PostId == postId);
        }

        public async Task<List<PostEntity>> ListAllAsync()
        {
            using ApplicationDatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Posts.AsNoTracking()
                    .Include(i => i.Comments).AsNoTracking()
                    .ToListAsync();
        }

        public async Task<List<PostEntity>> ListWithCommentsAsync()
        {
            using ApplicationDatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Posts.AsNoTracking()
                    .Include(i => i.Comments).AsNoTracking()
                    .Where(x => x.Comments != null && x.Comments.Any())
                    .ToListAsync();
        }

        public async Task<List<PostEntity>> ListWithLikesAsync(int numberOfLikes)
        {
            using ApplicationDatabaseContext context = _contextFactory.CreateDbContext();
            return await context.Posts.AsNoTracking()
                    .Include(i => i.Comments).AsNoTracking()
                    .Where(x => x.Likes >= numberOfLikes)
                    .ToListAsync();
        }

        public async Task UpdateAsync(PostEntity post)
        {
            using ApplicationDatabaseContext context = _contextFactory.CreateDbContext();
            context.Posts.Update(post);

            _ = await context.SaveChangesAsync();
        }
    }
}
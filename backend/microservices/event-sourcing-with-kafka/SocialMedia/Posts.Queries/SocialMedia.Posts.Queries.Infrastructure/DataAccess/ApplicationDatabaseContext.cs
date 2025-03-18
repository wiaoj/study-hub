using Microsoft.EntityFrameworkCore;
using SocialMedia.Posts.Queries.Domain.Entities;

namespace SocialMedia.Posts.Queries.Infrastructure.DataAccess;
public class ApplicationDatabaseContext(DbContextOptions options) : DbContext(options) {
    public DbSet<PostEntity> Posts { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
}
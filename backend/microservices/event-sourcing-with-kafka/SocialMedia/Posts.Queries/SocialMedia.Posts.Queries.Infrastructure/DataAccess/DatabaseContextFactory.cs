using Microsoft.EntityFrameworkCore;

namespace SocialMedia.Posts.Queries.Infrastructure.DataAccess;
public class DatabaseContextFactory(DbContextOptions dbContextOptions) {
    public ApplicationDatabaseContext CreateDbContext() {
        return new ApplicationDatabaseContext(dbContextOptions);
    }
}
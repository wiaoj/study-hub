using Microsoft.EntityFrameworkCore;

namespace Repository;
public sealed class ApplicationDbContext : DbContext {
    public DbSet<Person> People { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options) { }
}
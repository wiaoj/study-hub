using GraphQl.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQl.Data;
public class ApplicationDbContext : DbContext {
    public DbSet<Book> Books { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public void SeedData() {
        this.Books.AddRange(
                    new Book {
                        Id = 1,
                        Title = "The Hobbit",
                        Author = "J.R.R. Tolkien",
                        Published = new DateTime(1937, 9, 21),
                        Pages = 310
                    },
                    new Book {
                        Id = 2,
                        Title = "The Fellowship of the Ring",
                        Author = "J.R.R. Tolkien",
                        Published = new DateTime(1954, 7, 29),
                        Pages = 423
                    },
                    new Book {
                        Id = 3,
                        Title = "The Two Towers",
                        Author = "J.R.R. Tolkien",
                        Published = new DateTime(1954, 11, 11),
                        Pages = 352
                    },
                    new Book {
                        Id = 4,
                        Title = "The Return of the King",
                        Author = "J.R.R. Tolkien",
                        Published = new DateTime(1955, 10, 20),
                        Pages = 416
                    });
        this.SaveChanges();
    }
}
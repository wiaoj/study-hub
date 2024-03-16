using GraphQl.Data;
using GraphQl.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GraphQl.GraphQl;
public class Query {
    public Task<List<Book>> Books(ClaimsPrincipal claims, ApplicationDbContext context, String? title) {
        DbSet<Book> books = context.Books;
        //throw new Exception("Error retrieving data");
        return title is not null
            ? books.Where(x => x.Title.Contains(title, StringComparison.InvariantCultureIgnoreCase)).ToListAsync()
            : books.ToListAsync();
    }
}

public class BookType : ObjectType<Book> {
    protected override void Configure(IObjectTypeDescriptor<Book> descriptor) {
        descriptor
            .Field(x => x.Title)
            .ResolveWith<Resolvers>(x => x.GetTitle(default!))
            .Type<StringType>(); 
        
        descriptor
            .Field(x => x.Published)
            .ResolveWith<Resolvers>(x => x.GetFormattedDate(default!))
            .Type<StringType>();
    }
}
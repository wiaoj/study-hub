using GraphQl.Data;
using GraphQl.GraphQl;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextFactory<ApplicationDbContext>(options => {
    options.UseInMemoryDatabase("Books");
});


builder.Services.AddGraphQLServer()
                .RegisterDbContext<ApplicationDbContext>(DbContextKind.Synchronized)
                .AddQueryType<Query>()
                .AddType<BookType>();

builder.Services.AddAuthentication();

WebApplication app = builder.Build();

using(IServiceScope scope = app.Services.CreateScope()) {
    IServiceProvider services = scope.ServiceProvider;
    ApplicationDbContext context = services.GetRequiredService<ApplicationDbContext>();
    context.SeedData();
}

app.UseStaticFiles();
app.UseAuthentication();
app.MapGraphQL();

app.Run();

using Confluent.Kafka;
using CQRS.EventSourcing.Core.Consumers;
using CQRS.EventSourcing.Core.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Post.Query.Api.Queries;
using Post.Query.Infrastructure.Repositories;
using SocialMedia.Posts.Queries.Api.Queries;
using SocialMedia.Posts.Queries.Domain.Entities;
using SocialMedia.Posts.Queries.Domain.Repositories;
using SocialMedia.Posts.Queries.Infrastructure.Consumers;
using SocialMedia.Posts.Queries.Infrastructure.DataAccess;
using SocialMedia.Posts.Queries.Infrastructure.Dispatchers;
using SocialMedia.Posts.Queries.Infrastructure.Handlers;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Action<DbContextOptionsBuilder> configureDbContext;

configureDbContext = o => o.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));


builder.Services.AddDbContext<ApplicationDatabaseContext>(dbContextOptions => {
    dbContextOptions.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddSingleton<DatabaseContextFactory>(provider => {
    DbContextOptions options = provider.GetRequiredService<DbContextOptions>();
    return new DatabaseContextFactory(options);
});

// create database and tables
ApplicationDatabaseContext dataContext = builder.Services.BuildServiceProvider().GetRequiredService<ApplicationDatabaseContext>();
dataContext.Database.EnsureCreated();

builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IQueryHandler, QueryHandler>();
builder.Services.AddScoped<IEventHandler, SocialMedia.Posts.Queries.Infrastructure.Handlers.EventHandler>();
builder.Services.Configure<ConsumerConfig>(builder.Configuration.GetSection(nameof(ConsumerConfig)));
builder.Services.AddScoped<IEventConsumer, EventConsumer>();

// register query handler methods
IQueryHandler queryHandler = builder.Services.BuildServiceProvider().GetRequiredService<IQueryHandler>();
PostQueryDispatcher dispatcher = new();
dispatcher.RegisterHandler<FindAllPostsQuery>(queryHandler.HandleAsync);
dispatcher.RegisterHandler<FindPostByIdQuery>(queryHandler.HandleAsync);
dispatcher.RegisterHandler<FindPostsByAuthorQuery>(queryHandler.HandleAsync);
dispatcher.RegisterHandler<FindPostsWithCommentsQuery>(queryHandler.HandleAsync);
dispatcher.RegisterHandler<FindPostsWithLikesQuery>(queryHandler.HandleAsync);
builder.Services.AddScoped<IQueryDispatcher<PostEntity>>(_ => dispatcher);

builder.Services.AddControllers();
builder.Services.AddHostedService<ConsumerHostedService>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
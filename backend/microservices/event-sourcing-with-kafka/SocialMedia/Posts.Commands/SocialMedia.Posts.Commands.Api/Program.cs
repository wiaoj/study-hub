using Confluent.Kafka;
using CQRS.EventSourcing.Core.Domain;
using CQRS.EventSourcing.Core.Events;
using CQRS.EventSourcing.Core.Handlers;
using CQRS.EventSourcing.Core.Infrastructure;
using CQRS.EventSourcing.Core.Producers;
using MongoDB.Bson.Serialization;
using SocialMedia.Posts.Commands.Domain.Aggregates;
using SocialMedia.Posts.Commands.Infrastructure;
using SocialMedia.Posts.Commands.Infrastructure.Config;
using SocialMedia.Posts.Commands.Infrastructure.Handlers;
using SocialMedia.Posts.Commands.Infrastructure.Producers;
using SocialMedia.Posts.Commands.Infrastructure.Repositories;
using SocialMedia.Posts.Commands.Infrastructure.Stores;
using SocialMedia.Posts.Common.Events;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
BsonClassMap.RegisterClassMap<BaseEvent>();
BsonClassMap.RegisterClassMap<PostCreatedEvent>();
BsonClassMap.RegisterClassMap<MessageUpdatedEvent>();
BsonClassMap.RegisterClassMap<PostLikedEvent>();
BsonClassMap.RegisterClassMap<PostCommentCreatedEvent>();
BsonClassMap.RegisterClassMap<CommentUpdatedEvent>();
BsonClassMap.RegisterClassMap<CommentRemovedEvent>();
BsonClassMap.RegisterClassMap<PostRemovedEvent>();

// Add services to the container.
builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection(nameof(ProducerConfig)));
builder.Services.Configure<MessageQueueConfig>(builder.Configuration.GetSection(MessageQueueConfig.SectionName));
builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
builder.Services.AddScoped<IEventProducer, EventProducer>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddScoped<IEventSourcingHandler<PostAggregate, PostId>, EventSourcingHandler>();
//builder.Services.AddScoped<ICommandHandler<NewPostCommand>, NewPostCommandHandler>(); 

builder.Services.AddCommandHandlers(typeof(Program).Assembly);

// register command handler methods
//ICommandHandler<NewPostCommand> commandHandler = builder.Services.BuildServiceProvider().GetRequiredService<ICommandHandler<NewPostCommand>>();
//CommandDispatcher dispatcher = new();
//dispatcher.RegisterHandler<NewPostCommand>(commandHandler.HandleAsync);
////dispatcher.RegisterHandler<EditMessageCommand>(commandHandler.HandleAsync);
////dispatcher.RegisterHandler<LikePostCommand>(commandHandler.HandleAsync);
////dispatcher.RegisterHandler<AddCommentCommand>(commandHandler.HandleAsync);
////dispatcher.RegisterHandler<EditCommentCommand>(commandHandler.HandleAsync);
////dispatcher.RegisterHandler<RemoveCommentCommand>(commandHandler.HandleAsync);
////dispatcher.RegisterHandler<DeletePostCommand>(commandHandler.HandleAsync);
////dispatcher.RegisterHandler<RestoreReadDbCommand>(commandHandler.HandleAsync);
//builder.Services.AddSingleton<ICommandDispatcher>(_ => dispatcher);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

WebApplication app = builder.Build();

if(app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
await app.RunAsync();

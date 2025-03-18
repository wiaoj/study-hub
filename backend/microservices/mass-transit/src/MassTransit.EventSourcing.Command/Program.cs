using Carter;
using MassTransit;
using MassTransit.EventSourcing.Command.EventPublisher;
using MassTransit.EventSourcing.Command.Events;
using MassTransit.EventSourcing.Command.EventStore;
using MongoDB.Bson.Serialization;

var builder = WebApplication.CreateBuilder(args);


BsonClassMap.RegisterClassMap<BaseEvent>();
BsonClassMap.RegisterClassMap<PostCreatedEvent>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCarter();

builder.Services.AddMassTransit(x => {
    x.UsingRabbitMq((context, cfg) => {
        cfg.Host("amqps://fvitbcrq:KCmRh1SfarZoJNaKKoS5aRaSHZpcUTnq@sparrow.rmq.cloudamqp.com/fvitbcrq", h => {
            h.Username("fvitbcrq");
            h.Password("KCmRh1SfarZoJNaKKoS5aRaSHZpcUTnq");
            h.ConfigureBatchPublish(x =>
            {
                x.Enabled = true;
                x.Timeout = TimeSpan.FromMilliseconds(2);
            });
        });
    });
});
 
builder.Services.AddSingleton<IEventStoreRepository, MongoDbEventStoreRepository>();
builder.Services.AddScoped<IEventStore, InMemoryEventStore>();
builder.Services.AddScoped<IEventPublisher, MassTransitEventPublisher>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCarter();

await app.RunAsync(default(CancellationToken));

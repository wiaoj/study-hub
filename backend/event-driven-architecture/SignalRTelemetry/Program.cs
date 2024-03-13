using SignalRTelemetry;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddHostedService<RabbitMqConsumerService>();

WebApplication app = builder.Build();

app.MapHub<TelemetryHub>("/telemetryHub");
app.Run();
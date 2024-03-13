using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace SignalRTelemetry;
public class RabbitMqConsumerService : BackgroundService {
    private readonly IModel channel;
    private readonly ILogger<RabbitMqConsumerService> logger;
    private readonly IHubContext<TelemetryHub> hubContext;


    public RabbitMqConsumerService(ILogger<RabbitMqConsumerService> logger,
                                   IHubContext<TelemetryHub> hubContext) {

        ConnectionFactory connectionFactory = new() {
            HostName = "localhost",
            Port = 5672
        };
        IConnectionFactory factory = connectionFactory;
        IConnection connection = factory.CreateConnection();
        this.channel = connection.CreateModel();

        this.channel.QueueDeclare(queue: "telemetry",
                                  durable: false,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

        this.logger = logger;
        this.hubContext = hubContext;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken) {
        EventingBasicConsumer consumer = new(this.channel);
        consumer.Received += async (model, ea) => {
            Byte[] body = ea.Body.ToArray();
            String message = Encoding.UTF8.GetString(body);
            this.logger.LogInformation(" [x] Received {@message}", message);


            if(Int32.TryParse(message, out Int32 parseResult)) {
                TelemetryData telemetry = new(Int32.Parse(message));

                await this.hubContext.Clients.All.SendAsync("TelemetryReceived", telemetry.Decibels);
            }
            else {
                this.logger.LogWarning($"Decibels data {message} is not valid.");
            }
        };

        this.channel.BasicConsume(queue: "telemetry",
                                  autoAck: true,
                                  consumer: consumer);

        return Task.CompletedTask;
    }
}
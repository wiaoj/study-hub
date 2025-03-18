using CQRS.EventSourcing.Core.Consumers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SocialMedia.Posts.Queries.Infrastructure.Consumers;
public class ConsumerHostedService(ILogger<ConsumerHostedService> logger, IServiceProvider serviceProvider) : IHostedService {
    public Task StartAsync(CancellationToken cancellationToken) {
        logger.LogInformation("Event Consumer Service running.");

        using(IServiceScope scope = serviceProvider.CreateScope()) {
            IEventConsumer eventConsumer = scope.ServiceProvider.GetRequiredService<IEventConsumer>();
            String? topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");

            Task.Run(() => eventConsumer.Consume(topic), cancellationToken);
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) {
        logger.LogInformation("Event Consumer Service Stopped");

        return Task.CompletedTask;
    }
}
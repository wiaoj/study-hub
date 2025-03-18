using Carter;
using MassTransit.EventSourcing.Command.Domain;
using MassTransit.EventSourcing.Command.Events;
using MassTransit.EventSourcing.Command.EventStore;
using Microsoft.AspNetCore.Mvc;

namespace MassTransit.EventSourcing.Command.Endpoints;
public class NewPostEndpoint : ICarterModule {
    internal sealed record PostCreateRequest(String Text);
    public void AddRoutes(IEndpointRouteBuilder app) {
        app.MapPost("posts", async (PostCreateRequest request, IEventStore eventStore) => {

            PostAggregate post = PostAggregate.Create(request.Text);

            post.RaiseEvent(new PostCreatedEvent {
                Id = post.Id,
                AggregateIdentifier = post.Id,
                AggregateType = nameof(PostAggregate),
                Version = 1,
                Text = request.Text,
                Author = "Author",
                DatePosted = DateTime.Now
            });

            await eventStore.SaveEventsAsync(post.Events);
        });
    }
}

public class NewCommentEndpoint : ICarterModule {
    internal sealed record CommentCreateRequest([FromRoute(Name = "id")] String Id,
                                                [FromBody] String Text);
    public void AddRoutes(IEndpointRouteBuilder app) {
        app.MapPost("posts/{id}", async (CommentCreateRequest request, IEventStore eventStore) => {

            PostAggregate post = eventStore.GetAggregate<PostAggregate>(request.Id);

            return post;
        });
    }
}
using CQRS.EventSourcing.Core.Events;
using SocialMedia.Posts.Common.Events;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SocialMedia.Posts.Queries.Infrastructure.Converters;
public class EventJsonConverter : JsonConverter<BaseEvent> {
    public override Boolean CanConvert(Type type) {
        return type.IsAssignableFrom(typeof(BaseEvent));
    }

    public override BaseEvent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        if(!JsonDocument.TryParseValue(ref reader, out JsonDocument? doc))
            throw new JsonException($"Failed to parse {nameof(JsonDocument)}");

        if(!doc.RootElement.TryGetProperty("Type", out JsonElement type))
            throw new JsonException("Could not detect the Type discriminator property!");

        String? typeDiscriminator = type.GetString();
        String json = doc.RootElement.GetRawText();

        return typeDiscriminator switch {
            nameof(PostCreatedEvent) => JsonSerializer.Deserialize<PostCreatedEvent>(json, options),
            nameof(MessageUpdatedEvent) => JsonSerializer.Deserialize<MessageUpdatedEvent>(json, options),
            nameof(PostLikedEvent) => JsonSerializer.Deserialize<PostLikedEvent>(json, options),
            nameof(PostCommentCreatedEvent) => JsonSerializer.Deserialize<PostCommentCreatedEvent>(json, options),
            nameof(CommentUpdatedEvent) => JsonSerializer.Deserialize<CommentUpdatedEvent>(json, options),
            nameof(CommentRemovedEvent) => JsonSerializer.Deserialize<CommentRemovedEvent>(json, options),
            nameof(PostRemovedEvent) => JsonSerializer.Deserialize<PostRemovedEvent>(json, options),
            _ => throw new JsonException($"{typeDiscriminator} is not supported yet!")
        };
    }

    public override void Write(Utf8JsonWriter writer, BaseEvent value, JsonSerializerOptions options) {
        throw new NotImplementedException();
    }
}
namespace SocialMedia.Posts.Commands.Infrastructure.Config;
public sealed class MessageQueueConfig {
    public const String SectionName = "KafkaTopic";
    public required String Topic { get; set; }
}
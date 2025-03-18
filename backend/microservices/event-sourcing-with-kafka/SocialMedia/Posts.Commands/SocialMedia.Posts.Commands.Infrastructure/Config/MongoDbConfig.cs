namespace SocialMedia.Posts.Commands.Infrastructure.Config;
public sealed class MongoDbConfig {
    public required String ConnectionString { get; set; }
    public required String Database { get; set; }
    public required String Collection { get; set; }
}
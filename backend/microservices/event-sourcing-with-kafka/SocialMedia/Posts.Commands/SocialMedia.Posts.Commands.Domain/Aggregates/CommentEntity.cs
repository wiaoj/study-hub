namespace SocialMedia.Posts.Commands.Domain.Aggregates;
public sealed class CommentEntity {
    public CommentId Id { get; private set; }
    public String Text { get; private set; }
    public String Username { get; private set; }

    public CommentEntity(CommentId id, String username, String text) {
        this.Id = id;
        this.Username = username;
        this.Text = text;
    }

    public static CommentEntity CreateNew(String username, String text) {
        return new(CommentId.CreateNew(), username, text);
    }
}
using CQRS.EventSourcing.Core.Domain;
using SocialMedia.Posts.Common.Events;
using SocialMedia.Posts.Common.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace SocialMedia.Posts.Commands.Domain.Aggregates;
public class PostAggregate : AggregateRoot<PostId> {
    private Boolean active;
    private String author;
    private String message;
    private readonly List<CommentEntity> comments;

    public Boolean IsActive => this.active;
    public String Author => this.author;
    public String Message => this.message;
    public IReadOnlyCollection<CommentEntity> Comments => this.comments.AsReadOnly();


    public PostAggregate() { }

    private PostAggregate(PostId id, String author, String message) : base(id) {
        this.author = author;
        this.message = message;
        this.active = true;
        this.comments = [];
    }

    public static PostAggregate CreateNew(String author, String message) {
        PostAggregate post = new(PostId.CreateNew(), author, message);
        post.RaiseEvent(new PostCreatedEvent(post.Id.Value,
                                             post.Author,
                                             post.Message,
                                             DateTime.UtcNow));

        return post;
    }

    public void Apply(PostCreatedEvent @event) {
        this.active = true;
    }

    public void EditMessage(String message) {
        CheckActive(Constants.Messages.InactivePostEdit);

        if(String.IsNullOrWhiteSpace(message))
            throw new InvalidOperationException($"The value of {nameof(message)} cannot be null or empty. Please provide a valid {nameof(message)}!");

        RaiseEvent(new MessageUpdatedEvent(this.Id.Value, message));
    }

    //public void Apply(MessageUpdatedEvent @event) {
    //    this.message = @event.Message;
    //}

    public void LikePost() {
        CheckActive(Constants.Messages.InactivePostLike);

        RaiseEvent(new PostLikedEvent(this.Id.Value));
    }

    //public void Apply(PostLikedEvent @event) {
    //    _id = @event.Id;
    //}

    public void AddComment(CommentEntity comment) {
        CheckActive(Constants.Messages.InactivePostAddComment);

        if(String.IsNullOrWhiteSpace(comment.Text))
            throw new InvalidOperationException($"The value of {nameof(comment)} cannot be null or empty. Please provide a valid {nameof(comment)}!");


        RaiseEvent(new PostCommentCreatedEvent(this.Id.Value,
                                               comment.Id.Value,
                                               comment.Text,
                                               comment.Username,
                                               DateTime.UtcNow));
    }

    //public void Apply(PostCommentCreatedEvent @event) {
    //    _id = @event.Id;
    //    this.comments.Add(@event.CommentId, new Tuple<String, String>(@event.Comment, @event.Username));
    //}

    public void EditComment(CommentEntity comment) {
        CheckActive(Constants.Messages.InactivePostEditComment);

        if(!this.comments.Find(x => x.Id == comment.Id).Username.Equals(comment.Username, StringComparison.CurrentCultureIgnoreCase))
            throw new InvalidOperationException("You are not allowed to edit a comment that was made by another user!");

        RaiseEvent(new CommentUpdatedEvent(this.Id.Value,
                                           comment.Id.Value,
                                           comment.Text,
                                           comment.Username,
                                           DateTime.UtcNow));
    }

    //public void Apply(CommentUpdatedEvent @event) {
    //    _id = @event.Id;
    //    this.comments[@event.CommentId] = new Tuple<String, String>(@event.Comment, @event.Username);
    //}

    public void RemoveComment(CommentId commentId, String username) {
        CheckActive(Constants.Messages.InactivePostRemoveComment);

        if(!this.comments.Find(x => x.Id == commentId).Username.Equals(username, StringComparison.CurrentCultureIgnoreCase))
            throw new InvalidOperationException("You are not allowed to remove a comment that was made by another user!");

        RaiseEvent(new CommentRemovedEvent(this.Id.Value, commentId.Value));
    }

    //public void Apply(CommentRemovedEvent @event) {
    //    _id = @event.Id;
    //    this.comments.Remove(@event.CommentId);
    //}

    public void DeletePost(String username) {
        CheckActive(Constants.Messages.InactivePostDelete);

        if(!this.author.Equals(username, StringComparison.CurrentCultureIgnoreCase))
            throw new InvalidOperationException("You are not allowed to delete a post that was made by somebody else!");

        RaiseEvent(new PostRemovedEvent(this.Id.Value));
    }

    //public void Apply(PostRemovedEvent @event) {
    //    _id = @event.Id;
    //    this.active = false;
    //}


    private void CheckActive(String message) {
        if(this.active.IsFalse())
            throw new InvalidOperationException(message);
    }
}
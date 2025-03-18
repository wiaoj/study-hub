using SocialMedia.Posts.Common.Events;
using SocialMedia.Posts.Queries.Domain.Entities;
using SocialMedia.Posts.Queries.Domain.Repositories;

namespace SocialMedia.Posts.Queries.Infrastructure.Handlers;
public class EventHandler : IEventHandler {
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;

    public EventHandler(IPostRepository postRepository, ICommentRepository commentRepository) {
        this._postRepository = postRepository;
        this._commentRepository = commentRepository;
    }

    public async Task On(PostCreatedEvent @event) {
        PostEntity post = new() {
            PostId = @event.PostId,
            Author = @event.Author,
            DatePosted = @event.DatePosted,
            Message = @event.Message
        };

        await this._postRepository.CreateAsync(post);
    }

    public async Task On(MessageUpdatedEvent @event) {
        PostEntity post = await this._postRepository.GetByIdAsync(@event.PostId);

        if(post == null)
            return;

        post.Message = @event.Message;
        await this._postRepository.UpdateAsync(post);
    }

    public async Task On(PostLikedEvent @event) {
        PostEntity post = await this._postRepository.GetByIdAsync(@event.PostId);

        if(post == null)
            return;

        post.Likes++;
        await this._postRepository.UpdateAsync(post);
    }

    public async Task On(PostCommentCreatedEvent @event) {
        CommentEntity comment = new() {
            PostId = @event.PostId,
            CommentId = @event.CommentId,
            CommentDate = @event.CommentCreatedAt,
            Comment = @event.Comment,
            Username = @event.Username,
            Edited = false
        };

        await this._commentRepository.CreateAsync(comment);
    }

    public async Task On(CommentUpdatedEvent @event) {
        CommentEntity comment = await this._commentRepository.GetByIdAsync(@event.CommentId);

        if(comment == null)
            return;

        comment.Comment = @event.Comment;
        comment.Edited = true;
        comment.CommentDate = @event.EditDate;

        await this._commentRepository.UpdateAsync(comment);
    }

    public async Task On(CommentRemovedEvent @event) {
        await this._commentRepository.DeleteAsync(@event.CommentId);
    }

    public async Task On(PostRemovedEvent @event) {
        await this._postRepository.DeleteAsync(@event.PostId);
    }
}
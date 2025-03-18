using CQRS.EventSourcing.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Posts.Common.DTOs;
using SocialMedia.Posts.Queries.Api.DTOs;
using SocialMedia.Posts.Queries.Api.Queries;
using SocialMedia.Posts.Queries.Domain.Entities;

namespace SocialMedia.Posts.Queries.Api.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class PostLookupController(ILogger<PostLookupController> logger, IQueryDispatcher<PostEntity> queryDispatcher) : ControllerBase {


    [HttpGet]
    public async Task<ActionResult> GetAllPostsAsync() {
        try {
            List<PostEntity> posts = await queryDispatcher.SendAsync(new FindAllPostsQuery());
            return NormalResponse(posts);
        }
        catch(Exception ex) {
            const String SAFE_ERROR_MESSAGE = "Error while processing request to retrieve all posts!";
            return ErrorResponse(ex, SAFE_ERROR_MESSAGE);
        }
    }

    [HttpGet("byId/{postId}")]
    public async Task<ActionResult> GetByPostIdAsync(Guid postId) {
        try {
            List<PostEntity> posts = await queryDispatcher.SendAsync(new FindPostByIdQuery { Id = postId });

            return posts == null || !posts.Any()
                ? (ActionResult)NoContent()
                : Ok(new PostLookupResponse(
                     posts, "Successfully returned post!"));
        }
        catch(Exception ex) {
            const String SAFE_ERROR_MESSAGE = "Error while processing request to find post by ID!";
            return ErrorResponse(ex, SAFE_ERROR_MESSAGE);
        }
    }

    [HttpGet("byAuthor/{author}")]
    public async Task<ActionResult> GetPostsByAuthorAsync(String author) {
        try {
            List<PostEntity> posts = await queryDispatcher.SendAsync(new FindPostsByAuthorQuery { Author = author });
            return NormalResponse(posts);
        }
        catch(Exception ex) {
            const String SAFE_ERROR_MESSAGE = "Error while processing request to find posts by author!";
            return ErrorResponse(ex, SAFE_ERROR_MESSAGE);
        }
    }

    [HttpGet("withComments")]
    public async Task<ActionResult> GetPostsWithCommentsAsync() {
        try {
            List<PostEntity> posts = await queryDispatcher.SendAsync(new FindPostsWithCommentsQuery());
            return NormalResponse(posts);
        }
        catch(Exception ex) {
            const String SAFE_ERROR_MESSAGE = "Error while processing request to find posts with comments!";
            return ErrorResponse(ex, SAFE_ERROR_MESSAGE);
        }
    }

    [HttpGet("withLikes/{numberOfLikes}")]
    public async Task<ActionResult> GetPostsWithLikesAsync(Int32 numberOfLikes) {
        try {
            List<PostEntity> posts = await queryDispatcher.SendAsync(new FindPostsWithLikesQuery { NumberOfLikes = numberOfLikes });
            return NormalResponse(posts);
        }
        catch(Exception ex) {
            const String SAFE_ERROR_MESSAGE = "Error while processing request to find posts with likes!";
            return ErrorResponse(ex, SAFE_ERROR_MESSAGE);
        }
    }

    private ActionResult NormalResponse(List<PostEntity> posts) {
        if(posts == null || !posts.Any())
            return NoContent();

        Int32 count = posts.Count;
        return Ok(new PostLookupResponse(posts, $"Successfully returned {count} post{(count > 1 ? "s" : String.Empty)}!"));
    }

    private ActionResult ErrorResponse(Exception ex, String safeErrorMessage) {
        logger.LogError(ex, safeErrorMessage);

        return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse(safeErrorMessage));
    }
}
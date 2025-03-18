using CQRS.EventSourcing.Core.Exceptions;
using CQRS.EventSourcing.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Posts.Commands.Api.Features.AddComment;
using SocialMedia.Posts.Common.DTOs;

namespace SocialMedia.Posts.Commands.Api.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class AddCommentController : ControllerBase {
    private readonly ILogger<AddCommentController> logger;
    private readonly ICommandDispatcher<AddCommentCommand> commandDispatcher;

    public AddCommentController(ILogger<AddCommentController> logger, ICommandDispatcher<AddCommentCommand> commandDispatcher) {
        this.logger = logger;
        this.commandDispatcher = commandDispatcher;
    }

    [HttpPut("{postId}")]
    public async Task<ActionResult> AddCommentAsync(Guid postId, AddCommentCommand command, CancellationToken cancellationToken) {
        try {
            command.PostId = postId;
            await this.commandDispatcher.SendAsync(command, cancellationToken);

            return Ok(new BaseResponse("Add comment request completed successfully!"));
        }
        catch(InvalidOperationException ex) {
            this.logger.Log(LogLevel.Warning, ex, "Client made a bad request!");
            return BadRequest(new BaseResponse(ex.Message));
        }
        catch(AggregateNotFoundException ex) {
            this.logger.Log(LogLevel.Warning, ex, "Could not retrieve aggregate, client passed an incorrect post ID targetting the aggregate!");
            return BadRequest(new BaseResponse(ex.Message));
        }
        catch(Exception ex) {
            const String SAFE_ERROR_MESSAGE = "Error while processing request to add a comment to a post!";
            this.logger.Log(LogLevel.Error, ex, SAFE_ERROR_MESSAGE);

            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse(SAFE_ERROR_MESSAGE));
        }
    }
}
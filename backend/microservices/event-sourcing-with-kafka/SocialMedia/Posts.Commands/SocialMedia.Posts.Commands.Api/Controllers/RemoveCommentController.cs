using CQRS.EventSourcing.Core.Exceptions;
using CQRS.EventSourcing.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Posts.Commands.Api.Features.RemoveComment;
using SocialMedia.Posts.Common.DTOs;

namespace SocialMedia.Posts.Commands.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RemoveCommentController : ControllerBase {
    private readonly ILogger<RemoveCommentController> _logger;
    private readonly ICommandDispatcher<RemoveCommentCommand> _commandDispatcher;

    public RemoveCommentController(ILogger<RemoveCommentController> logger, ICommandDispatcher<RemoveCommentCommand> commandDispatcher) {
        this._logger = logger;
        this._commandDispatcher = commandDispatcher;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveCommentAsync(Guid id, RemoveCommentCommand command, CancellationToken cancellationToken) {
        try {
            command.PostId = id;
            await this._commandDispatcher.SendAsync(command, cancellationToken);

            return Ok(new BaseResponse("Remove comment request completed successfully!"));
        }
        catch(InvalidOperationException ex) {
            this._logger.Log(LogLevel.Warning, ex, "Client made a bad request!");
            return BadRequest(new BaseResponse(ex.Message));
        }
        catch(AggregateNotFoundException ex) {
            this._logger.Log(LogLevel.Warning, ex, "Could not retrieve aggregate, client passed an incorrect post ID targetting the aggregate!");
            return BadRequest(new BaseResponse(ex.Message));
        }
        catch(Exception ex) {
            const String SAFE_ERROR_MESSAGE = "Error while processing request to remove a comment from a post!";
            this._logger.Log(LogLevel.Error, ex, SAFE_ERROR_MESSAGE);

            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse(SAFE_ERROR_MESSAGE));
        }
    }
}
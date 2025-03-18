using CQRS.EventSourcing.Core.Exceptions;
using CQRS.EventSourcing.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Posts.Commands.Api.Features.LikePost;
using SocialMedia.Posts.Common.DTOs;

namespace SocialMedia.Posts.Commands.Api.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class LikePostController : ControllerBase {
    private readonly ILogger<LikePostController> _logger;
    private readonly ICommandDispatcher<LikePostCommand> _commandDispatcher;

    public LikePostController(ILogger<LikePostController> logger, ICommandDispatcher<LikePostCommand> commandDispatcher) {
        this._logger = logger;
        this._commandDispatcher = commandDispatcher;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> LikePostAsync(Guid id, CancellationToken cancellationToken) {
        try {
            await this._commandDispatcher.SendAsync(new LikePostCommand(id), cancellationToken);

            return Ok(new BaseResponse("Like post request completed successfully!"));
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
            const String SAFE_ERROR_MESSAGE = "Error while processing request to like a post!";
            this._logger.Log(LogLevel.Error, ex, SAFE_ERROR_MESSAGE);

            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse(SAFE_ERROR_MESSAGE));
        }
    }
}
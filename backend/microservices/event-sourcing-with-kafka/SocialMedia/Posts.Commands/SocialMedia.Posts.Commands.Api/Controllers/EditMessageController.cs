using CQRS.EventSourcing.Core.Exceptions;
using CQRS.EventSourcing.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Posts.Commands.Api.Features.EditMessage;
using SocialMedia.Posts.Common.DTOs;

namespace SocialMedia.Posts.Commands.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EditMessageController : ControllerBase {
    private readonly ILogger<EditMessageController> _logger;
    private readonly ICommandDispatcher<EditMessageCommand> _commandDispatcher;

    public EditMessageController(ILogger<EditMessageController> logger, ICommandDispatcher<EditMessageCommand> commandDispatcher) {
        this._logger = logger;
        this._commandDispatcher = commandDispatcher;
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> EditMessageAsync(Guid id, EditMessageCommand command, CancellationToken cancellationToken) {
        try {
            command.PostId = id;
            await this._commandDispatcher.SendAsync(command, cancellationToken);

            return Ok(new BaseResponse("Edit message request completed successfully!"));
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
            const String SAFE_ERROR_MESSAGE = "Error while processing request to edit the message of a post!";
            this._logger.Log(LogLevel.Error, ex, SAFE_ERROR_MESSAGE);

            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse(SAFE_ERROR_MESSAGE));
        }
    }
}
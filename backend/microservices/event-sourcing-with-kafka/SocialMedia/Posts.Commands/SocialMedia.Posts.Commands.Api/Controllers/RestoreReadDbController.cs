using CQRS.EventSourcing.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Posts.Commands.Api.Features.RestoreReadDb;
using SocialMedia.Posts.Common.DTOs;

namespace SocialMedia.Posts.Commands.Api.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class RestoreReadDbController : ControllerBase {
    private readonly ILogger<RestoreReadDbController> _logger;
    private readonly ICommandDispatcher<RestoreReadDbCommand> _commandDispatcher;

    public RestoreReadDbController(ILogger<RestoreReadDbController> logger, ICommandDispatcher<RestoreReadDbCommand> commandDispatcher) {
        this._logger = logger;
        this._commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    public async Task<ActionResult> RestoreReadDbAsync(CancellationToken cancellationToken) {
        try {
            await this._commandDispatcher.SendAsync(new RestoreReadDbCommand(), cancellationToken);

            return StatusCode(StatusCodes.Status201Created, new BaseResponse("Read database restore request completed successfully!"));
        }
        catch(InvalidOperationException ex) {
            this._logger.Log(LogLevel.Warning, ex, "Client made a bad request!");
            return BadRequest(new BaseResponse(ex.Message));
        }
        catch(Exception ex) {
            const String SAFE_ERROR_MESSAGE = "Error while processing request to restore read database!";
            this._logger.Log(LogLevel.Error, ex, SAFE_ERROR_MESSAGE);

            return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse(SAFE_ERROR_MESSAGE));
        }
    }
}
using CQRS.EventSourcing.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Posts.Commands.Api.DTOs;
using SocialMedia.Posts.Commands.Api.Features.NewPost;
using SocialMedia.Posts.Common.DTOs;

namespace SocialMedia.Posts.Commands.Api.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class NewPostController : ControllerBase {
    private readonly ILogger<NewPostController> _logger;
    private readonly ICommandDispatcher<NewPostCommand> _commandDispatcher;

    public NewPostController(ILogger<NewPostController> logger, ICommandDispatcher<NewPostCommand> commandDispatcher) {
        this._logger = logger;
        this._commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    public async Task<ActionResult> NewPostAsync(NewPostCommand command, CancellationToken cancellationToken) {
        Guid id = Guid.NewGuid();
        try {
            await this._commandDispatcher.SendAsync(command, cancellationToken);

            return StatusCode(StatusCodes.Status201Created, new NewPostResponse(id, "New post creation request completed successfully!"));
        }
        catch(InvalidOperationException ex) {
            this._logger.Log(LogLevel.Warning, ex, "Client made a bad request!");
            return BadRequest(new BaseResponse(ex.Message));
        }
        catch(Exception ex) {
            const String SAFE_ERROR_MESSAGE = "Error while processing request to create a new post!";
            this._logger.Log(LogLevel.Error, ex, SAFE_ERROR_MESSAGE);

            return StatusCode(StatusCodes.Status500InternalServerError, new NewPostResponse(id, SAFE_ERROR_MESSAGE));
        }
    }
}
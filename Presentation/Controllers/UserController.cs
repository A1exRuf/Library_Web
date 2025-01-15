using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UseCases.Users.Commands.Register;
using UseCases.Users.Commands.Login;
using UseCases.Users.Queries.GetUserById;

namespace Presentation.Controllers;

public sealed class UserController : ApiController
{
    [HttpGet("{userId:guid}")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser(Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(userId);

        var book = await Sender.Send(query, cancellationToken);

        return Ok(book);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = request.Adapt<RegisterCommand>();

        var userId = await Sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetUser), new { userId }, userId);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = request.Adapt<LoginCommand>();

        try
        {
            var userId = await Sender.Send(command, cancellationToken);
            return Ok(new { UserId = userId });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new { message = "Invalid email or password." });
        }
    }
}

using Core.Exceptions;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UseCases.Users.Commands.DeleteUser;
using UseCases.Users.Commands.Login;
using UseCases.Users.Commands.LoginWithRefreshToken;
using UseCases.Users.Commands.Register;
using UseCases.Users.Commands.UpdateUser;
using UseCases.Users.Queries;
using UseCases.Users.Queries.GetUserById;
using UseCases.Users.Queries.GetUsers;

namespace Presentation.Controllers;

public sealed class UserController : ApiController
{
    [HttpGet("user")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser(Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(userId);

        var book = await Sender.Send(query, cancellationToken);

        return Ok(book);
    }

    [HttpGet("users")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser(string? searchTerm,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize,
        CancellationToken cancellationToken)
    {
        var query = new GetUsersQuery(searchTerm, sortColumn, sortOrder, page, pageSize);

        var book = await Sender.Send(query, cancellationToken);

        return Ok(book);
    }

    [HttpPost("register")]
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

    [HttpPost("login")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(command, cancellationToken);

        return Ok(response);
    }

    [HttpPost("loginwithrefreshtoken")]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> LoginWithRefreshToken([FromBody] LoginWithRefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var response = await Sender.Send(command, cancellationToken);

        return Ok(response);
    }


    [HttpDelete]
    //[Authorize(Policy = "OnlyForAdmin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> DeleteUser(Guid userId, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteUserCommand(userId);
            var succes = await Sender.Send(command, cancellationToken);

            return NoContent();
        }
        catch (UserNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut]
    //[Authorize(Policy = "OnlyForAdmin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateUser(
        [FromBody] UpdateUserRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateUserCommand(
            request.UserId,
            request.Name,
            request.Email,
            request.Password,
            request.ConfirmPassword,
            request.Role);

        await Sender.Send(command, cancellationToken);

        return NoContent();
    }
}

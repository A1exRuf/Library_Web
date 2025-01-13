using UseCases.Authors.Commands.CreateAuthor;
using UseCases.Authors.Queries.GetAuthorById;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public sealed class AuthorsController : ApiController
{
    [HttpGet("{authorId:guid}")]
    [ProducesResponseType(typeof(AuthorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAuthor(Guid authorId, CancellationToken cancellationToken)
    {
        var query = new GetAuthorByIdQuery(authorId);

        var author = await Sender.Send(query, cancellationToken);

        return Ok(author);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> CreateAuthor(
        [FromBody] CreateAuthorRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.Adapt<CreateAuthorCommand>();

        var authorId = await Sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetAuthor), new { authorId }, authorId);
    }
}

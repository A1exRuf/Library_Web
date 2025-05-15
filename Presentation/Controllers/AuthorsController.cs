using Core.Exceptions;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UseCases.Authors.Commands.CreateAuthor;
using UseCases.Authors.Commands.DeleteAuthor;
using UseCases.Authors.Commands.UpdateAuthor;
using UseCases.Authors.Queries;
using UseCases.Authors.Queries.GetAuthorById;
using UseCases.Authors.Queries.GetAuthors;

namespace Presentation.Controllers;

public sealed class AuthorsController : ApiController
{
    [HttpGet("{authorId}", Name = "GetAuthor")]
    [ProducesResponseType(typeof(AuthorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAuthor(Guid authorId, CancellationToken cancellationToken)
    {
        var query = new GetAuthorByIdQuery(authorId);

        var author = await Sender.Send(query, cancellationToken);

        return Ok(author);
    }

    [HttpGet(Name = "GetAuthors")]
    [ProducesResponseType(typeof(AuthorResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAuthors(CancellationToken cancellationToken)
    {
        var query = new GetAuthorsQuery();

        var authors = await Sender.Send(query, cancellationToken);

        return Ok(authors);
    }

    [HttpPost(Name = "PostAuthor")]
    [Authorize(Policy = "OnlyForAdmin")]
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

    [HttpDelete(Name = "DeleteAuthor")]
    [Authorize(Policy = "OnlyForAdmin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> DeleteAuthor(Guid authorId, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteAuthorCommand(authorId);
            var succes = await Sender.Send(command, cancellationToken);

            return NoContent();
        }
        catch (AuthorNotFoundException e)
        {
            return Ok(e.Message);
        }
    }

    [HttpPut(Name = "PutAuthor")]
    [Authorize(Policy = "OnlyForAdmin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAuthor(
        [FromBody] UpdateAuthorRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.Adapt<UpdateAuthorCommand>();
        await Sender.Send(command, cancellationToken);

        return NoContent();
    }
}

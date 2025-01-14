using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UseCases.Books.Queries.GetBookById;
using UseCases.Books.Commands.CreateBook;

namespace Presentation.Controllers;
public sealed class BooksController : ApiController
{
    [HttpGet("{bookId:guid}")]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBook(Guid bookId, CancellationToken cancellationToken)
    {
        var query = new GetBookByIdQuery(bookId);

        var book = await Sender.Send(query, cancellationToken);

        return Ok(book);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> CreateBook(
        [FromBody] CreateBookRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.Adapt<CreateBookCommand>();

        var bookId = await Sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetBook), new { bookId }, bookId);
    }
}

using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UseCases.Books.Queries.GetBookById;
using UseCases.Books.Commands.CreateBook;
using UseCases.Books.Commands.DeleteBook;
using UseCases.Books.Commands.UpdateBook;

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

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> DeleteBook(Guid bookId, CancellationToken cancellationToken)
    {
        var command = new DeleteBookCommand(bookId);

        var succes = await Sender.Send(command, cancellationToken);

        if (!succes)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut("{bookId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateBook(
        Guid bookId,
        [FromBody] UpdateBookCommand request,
        CancellationToken cancellationToken)
    {
        if (bookId != request.BookId)
        {
            return BadRequest("BookId in the route does not match AuthorId in the request body.");
        }

        var success = await Sender.Send(request, cancellationToken);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}

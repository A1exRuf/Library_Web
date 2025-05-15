using Core.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UseCases.Books.Commands.CreateBook;
using UseCases.Books.Commands.DeleteBook;
using UseCases.Books.Commands.UpdateBook;
using UseCases.Books.Queries;
using UseCases.Books.Queries.GetBookById;
using UseCases.Books.Queries.GetBooks;

namespace Presentation.Controllers;
public sealed class BooksController : ApiController
{
    [HttpGet("{bookId}", Name = "GetBook")]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBook(
        Guid bookId, 
        CancellationToken cancellationToken)
    {
        var query = new GetBookByIdQuery(bookId);

        var book = await Sender.Send(query, cancellationToken);

        return Ok(book);
    }

    [HttpGet(Name = "GetBooks")]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBooks(
        string? searchTerm,
        [FromQuery] string?[] genre,
        [FromQuery] Guid?[] authorId,
        string? sortColumn,
        string? sortOrder,
        bool? showUnavailable,
        int page,
        int pageSize,
        CancellationToken cancellation)
    {
        var query = new GetBooksQuery(searchTerm, genre, authorId, showUnavailable, sortColumn, sortOrder, page, pageSize);

        var books = await Sender.Send(query, cancellation);

        return Ok(books);
    }

    [HttpPost(Name = "PostBook")]
    [Authorize(Policy = "OnlyForAdmin")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> CreateBook(
        [FromForm] CreateBookRequest request,
        CancellationToken cancellationToken)
    {
        Stream? imageStream = request.Image?.OpenReadStream();

        var command = new CreateBookCommand(
            request.Isbn,
            request.Title,
            request.Genree,
            request.Description,
            request.AuthorId,
            imageStream);

        var bookId = await Sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetBook), new { bookId }, bookId);
    }

    [HttpPut("{bookId}", Name = "PutBook")]
    [Authorize(Policy = "OnlyForAdmin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateBook(
        [FromForm] UpdateBookRequest request,
        CancellationToken cancellationToken)
    {
        Stream? imageStream = request.Image?.OpenReadStream();

        var command = new UpdateBookCommand(
            request.BookId,
            request.Isbn,
            request.Title,
            request.Genree,
            request.Description,
            request.AuthorId,
            imageStream);

        await Sender.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{bookId}", Name = "DeleteBook")]
    [Authorize(Policy = "OnlyForAdmin")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBook(Guid bookId, CancellationToken cancellationToken)
    {
        try
        {
            var command = new DeleteBookCommand(bookId);
            var succes = await Sender.Send(command, cancellationToken);

            return NoContent();
        }
        catch (BookNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}

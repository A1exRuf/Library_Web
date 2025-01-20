using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UseCases.Books.Commands.AddBookImage;
using UseCases.Books.Commands.CreateBook;
using UseCases.Books.Commands.DeleteBook;
using UseCases.Books.Commands.UpdateBook;
using UseCases.Books.Queries;
using UseCases.Books.Queries.GetBookById;
using UseCases.Books.Queries.GetBooks;
using UseCases.Books.Queries.GetImageByItsId;

namespace Presentation.Controllers;
public sealed class BooksController : ApiController
{
    [HttpGet("books/{bookId:guid}")]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBook(Guid bookId, CancellationToken cancellationToken)
    {
        var query = new GetBookByIdQuery(bookId);

        var book = await Sender.Send(query, cancellationToken);

        return Ok(book);
    }

    [HttpGet("books")]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBook(
        string? searchTerm,
        string? genre,
        Guid? authorId,
        string? sortColumn,
        string? sortOrder,
        int page,
        int pageSize,
        CancellationToken cancellation)
    {
        var query = new GetBooksQuery(searchTerm, genre, authorId, sortColumn, sortOrder, page, pageSize);

        var books = await Sender.Send(query, cancellation);

        return Ok(books);
    }

    [HttpGet("{imageId:guid}/image")]
    [ProducesResponseType(typeof(BookResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBookImage(Guid imageId, CancellationToken cancellationToken)
    {
        var query = new GetImageByItsIdQuery(imageId);

        var image = await Sender.Send(query, cancellationToken);

        return File(image.FileResponse.Stream, image.FileResponse.ContentType);
    }

    [HttpPost]
    //[Authorize(Policy = "OnlyForAdmin")]
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
    [Authorize(Policy = "OnlyForAdmin")]
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
    [Authorize(Policy = "OnlyForAdmin")]
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

    [HttpPost("{bookId:guid}/image")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddBookImage(
        Guid bookId,
        IFormFile file,
        CancellationToken cancellationToken)
    {
        if (file.Length == 0)
        {
            return BadRequest("The file is empty.");
        }

        using var stream = file.OpenReadStream();

        var command = new AddBookImageCommand(bookId, stream, file.ContentType);

        var imageId = await Sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetBook), new { bookId }, imageId);
    }
}

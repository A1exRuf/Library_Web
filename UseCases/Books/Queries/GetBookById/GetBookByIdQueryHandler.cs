using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using Mapster;
using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Queries.GetBookById;

internal sealed class GetBookByIdQueryHandler : IQueryHandler<GetBookByIdQuery, BookResponse>
{
    private readonly IBookRepository _bookRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetBookByIdQueryHandler(
        IBookRepository bookRepository, 
        ICurrentUserService currentUserService)
    {
        _bookRepository = bookRepository;
        _currentUserService = currentUserService;
    }

    public async Task<BookResponse> Handle(
        GetBookByIdQuery request,
        CancellationToken cancellationToken)
    {
        Book? book = await _bookRepository.GetByIdAsync(request.BookId);

        if (book == null)
        {
            throw new BookNotFoundException(request.BookId);
        }

        List<LinkDTO> links = GetLinks(book);

        var bookResponse = book.Adapt<BookResponse>() with { Links = links };

        return bookResponse;
    }

    private List<LinkDTO> GetLinks(Book? book)
    {
        var links = new List<LinkDTO>
        {
            new("self", $"/api/books/{book.Id}", "GET"),
            new("author", $"/api/authors/{book.Author.Id}", "GET"),
        };

        if (_currentUserService.Role == "Admin")
        {
            links.Add(new("update", $"/api/books/{book.Id}", "PUT"));
            links.Add(new("delete", $"/api/books/{book.Id}", "DELETE"));
        }

        return links;
    }
}

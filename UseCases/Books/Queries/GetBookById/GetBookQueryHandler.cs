using Core.Abstractions;
using Core.Entities;
using Core.Exceptions;
using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Queries.GetBookById;

internal sealed class GetBookQueryHandler : IQueryHandler<GetBookByIdQuery, BookResponse>
{
    private readonly IBookRepository _bookRepository;

    public GetBookQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
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

        BookResponse bookResponse = new(
            book.Id, 
            book.Isbn, 
            book.Title, 
            book.Genree, 
            book.Description,
            new BookAuthorDTO(
                book.Author.Id,
                book.Author.FirstName,
                book.Author.LastName,
                book.Author.DateOfBirth,
                book.Author.Country),
            book.IsAvailable,
            book.ImageUrl);

        return bookResponse;
    }
}

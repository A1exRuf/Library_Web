using Core.Abstractions;
using Core.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Data;
using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Queries.GetBookById;

internal sealed class GetBookQueryHandler : IQueryHandler<GetBookByIdQuery, BookResponse>
{
    private readonly IApplicationDbContext _context;

    public GetBookQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<BookResponse> Handle(
        GetBookByIdQuery request,
        CancellationToken cancellationToken)
    {
        var book = await _context
            .Books
            .Where(b => b.Id == request.BookId)
            .Select(b => new BookResponse(
                b.Id,
                b.Isbn,
                b.Title,
                b.Genree,
                b.Description,
                b.AuthorId,
                b.Author.FirstName,
                b.Author.LastName,
                b.Author.DateOfBirth,
                b.Author.Country,
                b.IsAvailable,
                b.ImageId))
            .FirstOrDefaultAsync(cancellationToken);

        if (book == null)
        {
            throw new BookNotFoundException(request.BookId);
        }

        return book;
    }
}

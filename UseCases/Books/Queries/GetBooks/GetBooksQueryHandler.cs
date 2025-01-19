using Core.Abstractions;
using Core.Entities;
using System.Linq.Expressions;
using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Queries.GetBooks;

public sealed class GetBooksQueryHandler : IQueryHandler<GetBooksQuery, PagedList<BookResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetBooksQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<PagedList<BookResponse>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Book> booksQuery = _context.Books;

        if(!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            booksQuery = booksQuery.Where(b =>
            b.Isbn.Contains(request.SearchTerm) ||
            b.Title.ToLower().Contains(request.SearchTerm.ToLower()));
        }

        if(request.SortOrder?.ToLower() == "desc")
        {
            booksQuery = booksQuery.OrderByDescending(GetSortProperty(request));
        } 
        else
        {
            booksQuery = booksQuery.OrderBy(GetSortProperty(request));
        }

        var bookResponsesQuery = booksQuery
            .Select(b => new BookResponse(
                b.Id,
                b.Isbn,
                b.Title,
                b.Genree,
                b.Description,
                b.AuthorId,
                b.IsAvailable,
                b.ImageId));

        var books = await PagedList<BookResponse>.CreateAsync(bookResponsesQuery,
            request.Page,
            request.PageSize);

        return books;
    }

    private static Expression<Func<Book, object>> GetSortProperty(GetBooksQuery request)
    {
        return request.SortColumn?.ToLower() switch
        {
            "genree" => b => b.Genree,
            "title" => b => b.Title,
            _ => b => b.Id
        };
    }
}
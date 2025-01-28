using Core.Abstractions;
using Core.Entities;
using System.Linq.Expressions;
using UseCases.Abstractions.Messaging;
using UseCases.Authors.Queries;

namespace UseCases.Books.Queries.GetBooks;

public sealed class GetBooksQueryHandler : IQueryHandler<GetBooksQuery, PagedList<BookResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly IBlobService _blobService;

    public GetBooksQueryHandler(IApplicationDbContext context, IBlobService blobService)
    { 
        _context = context;
        _blobService = blobService;
    }

    public async Task<PagedList<BookResponse>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Book> booksQuery = _context.Books;

        if (request.ShowUnavailable != true)
        {
            booksQuery = booksQuery.Where(b =>
            b.IsAvailable == true);
        }

        if (request.AuthorId.Any())
        {
            booksQuery = booksQuery.Where(b =>
            request.AuthorId.Contains(b.AuthorId));
        }

        if (request.Genre.Any())
        {
            booksQuery = booksQuery.Where(b =>
            request.Genre.Contains(b.Genree));
        }

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            string searhTerm = request.SearchTerm.ToLower();
            booksQuery = booksQuery.Where(b =>
            b.Isbn.Contains(searhTerm) ||
            b.Title.ToLower().Contains(searhTerm) ||
            b.Author.FirstName.ToLower().Contains(searhTerm) ||
            b.Author.LastName.ToLower().Contains(searhTerm));
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
                new AuthorDTO(
                    b.Author.Id,
                    b.Author.FirstName,
                    b.Author.LastName,
                    b.Author.DateOfBirth,
                    b.Author.Country),
                b.IsAvailable,
                b.ImageUrl));

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
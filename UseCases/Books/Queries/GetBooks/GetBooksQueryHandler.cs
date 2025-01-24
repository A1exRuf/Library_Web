using Core.Abstractions;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UseCases.Abstractions.Messaging;

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

        if(request.AuthorId.HasValue)
        {
            booksQuery = booksQuery.Where(b =>
            b.AuthorId == request.AuthorId);
        }

        if (request.Genre != null)
        {
            booksQuery = booksQuery.Where(b =>
            b.Genree == request.Genre);
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
                b.AuthorId,
                b.Author.FirstName,
                b.Author.LastName,
                b.Author.DateOfBirth,
                b.Author.Country,
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
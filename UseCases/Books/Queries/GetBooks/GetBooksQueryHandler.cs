using Core.Abstractions;
using Core.Common;
using Core.Entities;
using Mapster;
using System.Linq.Expressions;
using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Queries.GetBooks;

public sealed class GetBooksQueryHandler : IQueryHandler<GetBooksQuery, PagedList<BookResponse>>
{
    private readonly IBookRepository _bookRepository;

    public GetBooksQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<PagedList<BookResponse>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var booksQuery = _bookRepository.Query();

        booksQuery = FilterByAvailability(request, booksQuery);
        booksQuery = FilterByAuthor(request, booksQuery);
        booksQuery = FilterByGenre(request, booksQuery);
        booksQuery = Search(request, booksQuery);
        booksQuery = Sort(request, booksQuery);

        var bookResponsesQuery = booksQuery.ProjectToType<BookResponse>();

        var books = await _bookRepository.GetPagedAsync(bookResponsesQuery, request.Page, request.PageSize);

        return books;
    }

    private static IQueryable<Book> Sort(GetBooksQuery request, IQueryable<Book> booksQuery)
    {
        if (request.SortOrder?.ToLower() == "desc")
        {
            booksQuery = booksQuery.OrderByDescending(GetSortProperty(request));
        }
        else
        {
            booksQuery = booksQuery.OrderBy(GetSortProperty(request));
        }

        return booksQuery;
    }

    private static IQueryable<Book> Search(GetBooksQuery request, IQueryable<Book> booksQuery)
    {
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            string searhTerm = request.SearchTerm.ToLower();
            booksQuery = booksQuery.Where(b =>
            b.Isbn.Contains(searhTerm) ||
            b.Title.ToLower().Contains(searhTerm));
        }

        return booksQuery;
    }

    private static IQueryable<Book> FilterByGenre(GetBooksQuery request, IQueryable<Book> booksQuery)
    {
        if (request.Genre.Any())
        {
            booksQuery = booksQuery.Where(b =>
            request.Genre.Contains(b.Genree));
        }

        return booksQuery;
    }

    private static IQueryable<Book> FilterByAuthor(GetBooksQuery request, IQueryable<Book> booksQuery)
    {
        if (request.AuthorId.Any())
        {
            booksQuery = booksQuery.Where(b =>
            request.AuthorId.Contains(b.AuthorId));
        }

        return booksQuery;
    }

    private static IQueryable<Book> FilterByAvailability(GetBooksQuery request, IQueryable<Book> booksQuery)
    {
        if (request.ShowUnavailable != true)
        {
            booksQuery = booksQuery.Where(b =>
            b.IsAvailable == true);
        }

        return booksQuery;
    }

    private static Expression<Func<Book, object>> GetSortProperty(GetBooksQuery request)
    {
        return request.SortColumn?.ToLower() switch
        {
            "genree" => b => b.Genree,
            _ => b => b.Title,
        };
    }
}
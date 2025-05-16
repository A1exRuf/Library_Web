using Core.Abstractions;
using Core.Common;
using Core.Entities;
using Core.Filters;
using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Queries.GetBooks;

public sealed class GetBooksQueryHandler : IQueryHandler<GetBooksQuery, PagedList<BookResponse>>
{
    private readonly IRepository<Book> _bookRepository;

    public GetBooksQueryHandler(IRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<PagedList<BookResponse>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var filter = new BooksFilter { 
            SearchTerm = request.SearchTerm, 
            Genres = request.Genre, 
            Authors = request.AuthorId, 
            ShowUnavailable = request.ShowUnavailable };

        var response = await _bookRepository.GetPagedListAsync<BookResponse, BooksFilter>(
            request.Page,
            request.PageSize,
            filter,
            x => x.Title,
            descending: false,
            asNoTracking: true,
            cancellationToken);

        return response;
    }
}
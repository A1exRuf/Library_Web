using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Queries.GetBooks;

public record GetBooksQuery(
    string? SearchTerm,
    string? Genre,
    Guid? AuthorId,
    string? SortColumn, 
    string? SortOrder, 
    int Page, 
    int PageSize) : IQuery<PagedList<BookResponse>>;

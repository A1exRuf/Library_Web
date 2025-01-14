using UseCases.Abstractions.Messaging;

namespace UseCases.Books.Queries.GetBookById;

public sealed record GetBookByIdQuery(Guid BookId) : IQuery<BookResponse>;

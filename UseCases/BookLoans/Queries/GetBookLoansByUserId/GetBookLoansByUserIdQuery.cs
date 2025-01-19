using UseCases.Abstractions.Messaging;

namespace UseCases.BookLoans.Queries.GetBookLoanById;

public record GetBookLoansByUserIdQuery(
    Guid UserId,
    int Page,
    int PageSize) : IQuery<PagedList<BookLoanResponse>>;

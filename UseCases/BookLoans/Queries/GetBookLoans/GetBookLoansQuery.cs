using Core.Common;
using UseCases.Abstractions.Messaging;

namespace UseCases.BookLoans.Queries.GetBookLoanById;

public record GetBookLoansQuery(
    int Page,
    int PageSize) : IQuery<PagedList<BookLoanResponse>>;

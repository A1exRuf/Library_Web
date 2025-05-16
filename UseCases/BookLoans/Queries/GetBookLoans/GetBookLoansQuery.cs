using UseCases.Abstractions.Messaging;

namespace UseCases.BookLoans.Queries.GetBookLoans;

public record GetBookLoansQuery() : IQuery<List<BookLoanResponse>>;

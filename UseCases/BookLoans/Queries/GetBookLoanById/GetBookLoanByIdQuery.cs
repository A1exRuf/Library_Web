using UseCases.Abstractions.Messaging;

namespace UseCases.BookLoans.Queries.GetBookLoanById;

public sealed record GetBookLoanByIdQuery(Guid BookLoanId) : IQuery<BookLoanResponse>;

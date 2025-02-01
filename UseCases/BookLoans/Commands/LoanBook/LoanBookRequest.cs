namespace UseCases.BookLoans.Commands.LoanBook;

public sealed record BookLoanRequest(Guid BookId, Guid UserId);

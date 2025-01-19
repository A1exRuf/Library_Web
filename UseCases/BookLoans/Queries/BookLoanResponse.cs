namespace UseCases.BookLoans.Queries;

public record BookLoanResponse(Guid Id, Guid UserId, Guid BookId, DateTime LoanDate, DateTime DueDate);

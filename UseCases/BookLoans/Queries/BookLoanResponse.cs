using Core.Common;

namespace UseCases.BookLoans.Queries;

public record BookLoanResponse(
    Guid Id, 
    BookLoansBookDTO Book, 
    DateTime LoanDate, 
    DateTime DueDate,
    List<Link> Links);

public record BookLoansBookDTO(
    Guid Id, 
    string Isbn, 
    string Title, 
    string? ImageURL, 
    BookLoansAuthorDTO Author );

public record BookLoansAuthorDTO(
    string FirstName, 
    string LastName);
